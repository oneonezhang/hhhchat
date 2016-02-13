using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace hhhchatServer
{
    public partial class FormServer : Form
    {
        private Socket serverSocket = null;
        private Dictionary<string, Socket> clientDir = new Dictionary<string, Socket>();
        private bool serverSocketActive = true;

        public FormServer()
        {
            InitializeComponent();
            ListBox.CheckForIllegalCrossThreadCalls = false;
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666);
            serverSocket.Bind(ipep);
            serverSocket.Listen(10);

            //show IP and Port of Server
            textBox1.Text = ipep.Address.ToString();   
            textBox2.Text = ipep.Port.ToString();

            //Create a new thread
            ThreadPool.QueueUserWorkItem(new WaitCallback(ConnectAccept));
        }

        private void ConnectAccept(object state)
        {
            while (serverSocketActive)
            {
                Socket newSocket = serverSocket.Accept();
                //set buffersize
                newSocket.SendBufferSize = 1024;
                newSocket.ReceiveBufferSize = 1024;

                byte[] nameByte = new byte[1024];
                try
                {
                    newSocket.Receive(nameByte);
                    string srcClient = Encoding.UTF8.GetString(nameByte).Trim(new char[] { '\0' });
                    //add a new client with a name not existing before
                    if (clientDir.ContainsKey(srcClient))
                    {
                        //reply 
                        byte[] answerByte = new byte[1024];
                        Encoding.UTF8.GetBytes("NameErr", 0, 7, answerByte, 0);
                        newSocket.Send(answerByte);
                        newSocket.Close();
                    }
                    else
                    {
                        //reply
                        byte[] answerByte = new byte[1024];
                        Encoding.UTF8.GetBytes("NameSuc", 0, 7, answerByte, 0);
                        newSocket.Send(answerByte);

                        //inform others
                        clientDir.Add(srcClient, newSocket);
                        clientlist.Items.Add(srcClient);
                        ArrayList nameWithSocket = new ArrayList(2);
                        nameWithSocket.Add(newSocket);
                        nameWithSocket.Add(nameByte);
                        foreach (Socket socket in clientDir.Values)
                        {
                            UpdateList(socket);
                        }
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveMsg), nameWithSocket);
                    }
                }
                catch(Exception) { }
            }
        }

        /* The first byte of message is flag:
        flag=0: common text 
                format-- 0+(receiver)senderName+time+text
        flag=1:file transfer
                format-- 1+(receiver)senderName+filename+filelength+text
        flag=2:enter chat form
                format-- 2
        flag=3:client list
                format-- 3+client1|client2|...
        */
        private void ReceiveMsg(object arrayList)
        {
            while (true)
            {
                ArrayList nameWithSocket = arrayList as ArrayList;
                Socket newSocket = nameWithSocket[0] as Socket;
                byte[] nameByte = nameWithSocket[1] as byte[];
                string srcClient = Encoding.UTF8.GetString(nameByte).Trim(new char[] { '\0' });

                byte[] flag = new byte[1024];
                try
                {
                    //Receive only 1 byte as flag
                    newSocket.Receive(flag);
                }
                catch(SocketException)
                {
                    //When the client quitted
                    if(clientDir.ContainsKey(srcClient))
                    {
                        
                        clientDir.Remove(srcClient);
                        clientlist.Items.Remove(srcClient);
                        foreach (Socket socket in clientDir.Values)
                        {
                            UpdateList(socket);
                        }
                        newSocket.Close();
                    }
                    break;
                }

                switch (flag[0])
                {
                    case 0://common file
                        {
                            byte[] desClientByte = new byte[1024];
                            newSocket.Receive(desClientByte);
                            string desClient = Encoding.UTF8.GetString(desClientByte).Trim(new char[] { '\0' });

                            //Forward the message
                            clientDir[desClient].Send(flag);
                            clientDir[desClient].Send(nameByte);
                            byte[] timeByte=new byte[1024];
                            newSocket.Receive(timeByte);
                            clientDir[desClient].Send(timeByte);
                            byte[] textByte = new byte[1024];
                            newSocket.Receive(textByte);
                            clientDir[desClient].Send(textByte);
                            break;
                        }

                    case 1://file transfer
                        {
                            byte[] desClientByte = new byte[1024];
                            newSocket.Receive(desClientByte);
                            string desClient = Encoding.UTF8.GetString(desClientByte).Trim(new char[] { '\0' });

                            try
                            {
                                clientDir[desClient].Send(flag);
                                clientDir[desClient].Send(nameByte);
                                byte[] fileNameByte = new byte[1024];
                                newSocket.Receive(fileNameByte);
                                clientDir[desClient].Send(fileNameByte);
                                byte[] fileLengthByte = new byte[1024];
                                newSocket.Receive(fileLengthByte);
                                long fileLength = BitConverter.ToInt64(fileLengthByte, 0);
                                clientDir[desClient].Send(fileLengthByte);
                                //Forward the file
                                long read = 0;
                                byte[] buffer = new byte[1024];
                                while (read < fileLength)
                                {
                                    int length = (fileLength - read > 1024) ? 1024 : (int)(fileLength - read);
                                    int readOnce = newSocket.Receive(buffer,length,SocketFlags.None);
                                    read += readOnce;
                                    int sent =clientDir[desClient].Send(buffer,readOnce,SocketFlags.None);
                                    Array.Clear(buffer, 0, buffer.Length);
                                }
                            }
                            catch (SocketException)
                            {
                                if (clientDir.ContainsKey(desClient))
                                {
                                    clientDir.Remove(desClient);
                                    clientlist.Items.Remove(desClient);
                                    foreach (Socket socket in clientDir.Values)
                                    {
                                        UpdateList(socket);
                                    }
                                }
                            }
                            break;
                        }

                    case 2://enter chat form
                        {
                            UpdateList(newSocket);
                            break;
                        }
                }
            }
        }

        private void UpdateList(Socket socket)
        {
            String nameList = "";
            string key="";
            foreach(string name in clientDir.Keys)
            {
                //get a name string except the client's own name
                if(clientDir[name]!=socket)
                {
                    if (nameList.Length!=0)
                    {
                        nameList+="|";
                    }
                    nameList+=name;
                }
                else
                {
                    key = name;
                }
            }
            //flag=3:client list
            byte[] nameListByte = new byte[1024];
            Encoding.UTF8.GetBytes(nameList,0,nameList.Length,nameListByte,0);
            try
            {
                byte[] flag = new byte[1024];
                flag[0] = 3;
                socket.Send(flag);
                socket.Send(nameListByte);
            }
            catch (SocketException)
            { }
        }

        private void Form1_FormClosed(object sender, FormClosingEventArgs e)
        {
            serverSocketActive = false;
        }
    }
}



using System;
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

namespace hhhchatClient
{
    public partial class Form2 : Form
    {
        private string userName;
        private Socket clientSocket;
        private Thread thread = null;
        private Dictionary<string, string> messageRecord = new Dictionary<string, string>();

        public Form2(string initialUserName, Socket initialClientSocket)
        {

            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            userName = initialUserName;
            clientSocket = initialClientSocket;
            lbMyName.Text = userName;
            thread = new Thread(ReceiveMsg);
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //Make sure to receive the client list
            byte[] flag = new byte[1024];
            flag[0] = 2;
            clientSocket.Send(flag);
        }

        /* The first byte of message is flag:
        flag=0: common text 
                format-- 0+(sender)receiverName+time+text
        flag=1:file transfer
                format-- 1+(sender)receiverName+filename+filelength+text
        flag=2:enter chat form
                format-- 2
        flag=3:client list
                format-- 3+client1|client2|...
        */
        private void ReceiveMsg(object state)
        {
            byte[] flag = new byte[1024];
            while (true)
            {
                try
                {
                    clientSocket.Receive(flag);
                }
                catch (SocketException)
                {
                    clientSocket.Close();
                    MessageBox.Show("Disconnected to the server");
                    this.Close();
                    Application.Exit();
                }

                switch (flag[0])
                {
                    case 0://common text
                        {
                            byte[] senderNameByte = new byte[1024];
                            clientSocket.Receive(senderNameByte);
                            string senderName = Encoding.UTF8.GetString(senderNameByte).Trim(new char[] { '\0' });

                            //Receive the message
                            byte[] timeByte = new byte[1024];
                            clientSocket.Receive(timeByte);
                            string time = Encoding.UTF8.GetString(timeByte).Trim(new char[] { '\0' });
                            byte[] textByte = new byte[1024];
                            clientSocket.Receive(textByte);
                            string text = Encoding.UTF8.GetString(textByte).Trim(new char[] { '\0' });

                            AddMsg(senderName, senderName + "\t" + time + ":");
                            AddMsg(senderName, text);

                            //You are not talking at present
                            if (senderName != chatter.Text)
                            {
                                notice.Text = "Notice: " + senderName + " has sent you a message";
                            }
                            break;
                        }
                    case 1://file transfer
                        {
                            byte[] senderNameByte = new byte[1024];
                            clientSocket.Receive(senderNameByte);
                            string senderName = Encoding.UTF8.GetString(senderNameByte).Trim(new char[] { '\0' });
                            byte[] fileNameByte = new byte[1024];
                            clientSocket.Receive(fileNameByte);
                            string fileName = Encoding.UTF8.GetString(fileNameByte).Trim(new char[] { '\0' });
                            byte[] fileLengthByte = new byte[1024];
                            clientSocket.Receive(fileLengthByte);
                            long fileLength = BitConverter.ToInt64(fileLengthByte, 0);

                            //Save the file
                            MessageBox.Show(senderName + " send you a file",userName);
                            SaveFileDialog saveFile = new SaveFileDialog();
                            saveFile.Filter = "All files（*.*)|*.*";
                            saveFile.FileName = fileName;
                            bool accept = false;
                            if (saveFile.ShowDialog() == DialogResult.OK)
                            {
                                string filePath = saveFile.FileName;
                                FileStream fs = new FileStream(filePath, FileMode.Create);
                                fs.Dispose();
                                accept = true;
                            }
                            long read = 0;
                            byte[] buffer = new byte[1024];
                            while (read < fileLength)
                            {
                                int readOnce = clientSocket.Receive(buffer);
                                read += readOnce;
                                if (accept)
                                {
                                    string filePath = saveFile.FileName;
                                    using (FileStream fs = new FileStream(filePath, FileMode.Append))
                                    {
                                        fs.Write(buffer, 0, readOnce);
                                        fs.Flush();
                                    }
                                }
                            }
                            MessageBox.Show("Success!");
                            break;
                        }

                    case 3://client list
                        {
                            byte[] nameListByte = new byte[1024];
                            clientSocket.Receive(nameListByte);
                            string nameList = Encoding.UTF8.GetString(nameListByte).Trim(new char[] { '\0' });
                            string[] nameArray = nameList.Split(new char[] { '|' });

                            //clear to get a new one
                            buddylist.Items.Clear();
                            foreach (string name in nameArray)
                            {
                                buddylist.Items.Add(name);
                                if (!messageRecord.ContainsKey(name))
                                {
                                    messageRecord.Add(name, "");
                                }
                            }
                            break;
                        }
                }

            }
        }

        private void AddMsg(string name, string msg)
        {
            if (messageRecord[name] != "")
            {
                messageRecord[name] += "\r\n";
            }
            messageRecord[name] = messageRecord[name] + msg;//update the messageRecord
            if (name == chatter.Text)
            {
                //you are talking at present
                textAll.Text = messageRecord[chatter.Text];
            }
        }

        //Send Common Message
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(buddylist.Text))
            {
                MessageBox.Show("Choose at least one buddy~");
            }
            else if (textSend.Text == "")
            {
                MessageBox.Show("The text can't be empty.");
            }
            else
            {
                  try
                  {
                    foreach (object i in buddylist.SelectedItems)
                    {
                        byte[] flag = new byte[1024];
                        flag[0] = 0;
                        clientSocket.Send(flag);
                        string desName = i.ToString();
                        byte[] desNameByte = new byte[1024];
                        Encoding.UTF8.GetBytes(desName, 0, desName.Length, desNameByte, 0);
                        clientSocket.Send(desNameByte);
                        string time = DateTime.Now.ToString();
                        byte[] timeByte = new byte[1024];
                        Encoding.UTF8.GetBytes(time, 0, time.Length, timeByte, 0);
                        clientSocket.Send(timeByte);
                        string text = textSend.Text;
                        byte[] textByte = new byte[1024];
                        Encoding.UTF8.GetBytes(text, 0, text.Length, textByte, 0);
                        clientSocket.Send(textByte);
                        AddMsg(desName, userName + "\t" + time + ":");
                        AddMsg(desName, textSend.Text);
                        chatter.Text = desName;
                    }
                }
                catch (SocketException)
                {
                        MessageBox.Show("Disconnected to the server.");
                        clientSocket.Close();
                        this.Close();
                        Application.Exit();
                }       
                textSend.Text = "";
            }
        }

        //Send files
        private void btnFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(buddylist.Text)||buddylist.SelectedItems.Count>1)
            {
                MessageBox.Show("Choose one buddy~");
            }
            else if (textFile.Text == "")
            {
                MessageBox.Show("Please choose a file.");
                return;
            }
            else
            {
                try
                {
                    byte[] flag = new byte[1024];
                    flag[0] = 1;
                    clientSocket.Send(flag);
                    string desName = buddylist.Text;
                    byte[] desNameByte = new byte[1024];
                    Encoding.UTF8.GetBytes(desName, 0, desName.Length, desNameByte, 0);
                    clientSocket.Send(desNameByte);
                    byte[] buffer = new byte[1024];
                    string filePath = textFile.Text;
                    long send = 0;
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        //send information about the file
                        long fileLength = fs.Length;
                        string fileName = Path.GetFileName(filePath);
                        byte[] fileNameByte = new byte[1024];
                        Encoding.UTF8.GetBytes(fileName, 0, fileName.Length, fileNameByte, 0);
                        clientSocket.Send(fileNameByte);
                        byte[] fileLengthByte = new byte[1024];
                        fileLengthByte = BitConverter.GetBytes(fileLength);
                        clientSocket.Send(fileLengthByte);

                        //cut the file into pieces and send
                        while (send < fileLength)
                        {
                            int read = fs.Read(buffer, 0, 1024);
                            int sendOnce = clientSocket.Send(buffer,read,SocketFlags.None);
                            send += sendOnce;
                        }
     
                    }
                }
                catch (SocketException)
                {
                    MessageBox.Show("Disconnected to the server.");
                    clientSocket.Close();
                    this.Close();
                    Application.Exit();
                }
            }
        }

        //select a file
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                string filePath = openfile.FileName;
                textFile.Text = filePath;
            }
        }

        //change the "window" when change the chatter
        private void buddylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (buddylist.SelectedItems.Count == 1)
            {
                textAll.Text = messageRecord[buddylist.Text];
                chatter.Text = buddylist.Text;
                //You have opened the "window",so the notice closes
                if (notice.Text.Contains(chatter.Text))
                {
                    notice.Text = "";
                }
            }
        }

        //Press Enter to send message
        private void textSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                btnSend_Click(sender, e);
            }
        }

        //ctrl+enter to change to a new line
        private void textSend_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyData == (Keys.Control & Keys.Enter))
            {
                textSend.AppendText("\r\n");
            }
        }

        //group send
        private void group_CheckedChanged(object sender, EventArgs e)
        {
            if (group.Checked == true)
            {
                buddylist.SelectionMode = SelectionMode.MultiSimple;
            }
            else
            {
                buddylist.SelectionMode = SelectionMode.One;
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientSocket.Close();
            thread.Abort();
            Application.Exit();
        }

        //show the up-to-date message
        private void textAll_TextChanged(object sender, EventArgs e)
        {
            textAll.SelectionStart = textAll.TextLength;
            textAll.ScrollToCaret();
        }
    }
}


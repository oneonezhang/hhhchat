using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace hhhchatClient
{
    public partial class Form1 : Form
    {
        internal string userName;
        internal Socket clientSocket=null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666);
            clientSocket.SendBufferSize = 1024;
            clientSocket.ReceiveBufferSize = 1024;
            try
            {
                clientSocket.Connect(ipep);

                Regex reg = new Regex(@"^[a-zA-Z0-9_]+$");
                userName = textName.Text;
                //Check whether the name only consisits of letters,numbers,underlines
                if (reg.IsMatch(userName))
                {

                    byte[] nameByte = new byte[1024];
                    Encoding.UTF8.GetBytes(userName,0,userName.Length,nameByte,0);
                    clientSocket.Send(nameByte);
                    byte[] answerByte = new byte[1024];
                    clientSocket.Receive(answerByte);
                    string answer = Encoding.UTF8.GetString(answerByte).Trim(new char[] { '\0' });
                    if (answer == "NameErr")
                    {
                        //fail to log in
                        MessageBox.Show(userName + "has been used.\r\nPlease try another one.");
                        textName.Clear();
                        clientSocket.Close();
                    }
                    else if (answer == "NameSuc")
                    {
                        //log in successfully
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("Only letters,numbers,underlines are allowed");
                    textName.Clear();
                    clientSocket.Close();
                }

            }
            catch (SocketException)
            {
                //When the server isn't started
                MessageBox.Show("Disconnected to the server");
                this.Close();
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}

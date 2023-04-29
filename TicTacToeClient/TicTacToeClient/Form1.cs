using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TicTacToeClient
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;
        Socket clientSocket;




        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form_Close);
            InitializeComponent();
        }

        private void Form_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void btnconnect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = txtbxIP.Text;

            int portNum;
            if (Int32.TryParse(txtbxPort.Text, out portNum))
            {
                try
                {
                    Byte[] name_buffer = Encoding.Default.GetBytes(txtbxName.Text);
                    Byte[] port_buffer = new byte[4096];
                    clientSocket.Connect(IP, portNum);

                    clientSocket.Send(name_buffer);

                    clientSocket.Receive(port_buffer);
                    string callback = Encoding.Default.GetString(port_buffer);
                    callback = callback.Replace("\0", "");
                    if (callback == ":01:")
                    { //Connection Accepted 
                        txtbxIP.Enabled = false;
                        txtbxPort.Enabled = false;
                        btnconnect.Enabled = false;
                        btnconnect.BackColor = Color.Green;
                        btnDisconnect.Enabled = true;
                        txtbxName.Enabled = false;

                        ClientRichTxtBox.AppendText("Connected to the server!\n");
                        connected = true;
                        Thread recieveClientThread = new Thread(() => RecieveFromServer());
                        recieveClientThread.Start();
                    }
                    else //Connection Denied
                    {
                        ClientRichTxtBox.AppendText("Username already taken!");

                    }
                }
                catch
                {
                    ClientRichTxtBox.AppendText("Cannot connect to the server!\n");
                }
            }
            else
            {
                ClientRichTxtBox.AppendText("Invalid Port!\n");
            }

        }

        private void RecieveFromServer()
        {
            while (connected)
            {
                try
                {
                    Byte[] recieveBuffer = new Byte[512];
                    clientSocket.Receive(recieveBuffer);

                    string incomingmessage = Encoding.Default.GetString(recieveBuffer);
                    incomingmessage = incomingmessage.Replace("\0", "");

                    string messagetype = incomingmessage.Substring(0, 4);
                    incomingmessage = incomingmessage.Substring(4);
                    if (messagetype.Substring(0, 2) == ":0") //Message type name verification
                    {
                        if (messagetype.Substring(2, 2) == "1:")
                        { //Success
                            txtbxName.Enabled = false;

                            txtbxchoice.Enabled = true;
                            btnchoice.Enabled = true;

                            ClientRichTxtBox.AppendText("Welcome, " + incomingmessage + "!\n");
                        }
                        else
                        {
                            ClientRichTxtBox.AppendText("Username already taken!\n");
                        }
                    }
                    else if (messagetype.Substring(0, 4) == ":11:")
                    { //Recieve Board
                        ClientRichTxtBox.AppendText(incomingmessage + "\n");

                    }
                    else if (messagetype.Substring(0, 2) == ":3") //Disconnect 
                    {
                        if (messagetype.Substring(2, 2) == "1:")
                        { //Success
                            connected = false;
                            terminating = true;


                            txtbxIP.Enabled = true;
                            txtbxPort.Enabled = true;
                            btnconnect.Enabled = true;

                            btnconnect.BackColor = Control.DefaultBackColor;
                            btnDisconnect.Enabled = false;
                            txtbxName.Enabled = false; txtbxName.Clear();
                            btnchoice.Enabled = false;
                            txtbxchoice.Enabled = false; txtbxchoice.Clear();

                            ClientRichTxtBox.AppendText("Disconnected from Server!\n");

                        }
                        else
                        { //Cannot Quit the game
                            ClientRichTxtBox.AppendText("Server doesn't allow discconnection!\n");
                        }
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        ClientRichTxtBox.AppendText("The server has disconnected!\n");


                        txtbxIP.Enabled = true;
                        txtbxPort.Enabled = true;
                        btnconnect.Enabled = true;

                        btnconnect.BackColor = Control.DefaultBackColor;
                        btnDisconnect.Enabled = false;
                        txtbxName.Enabled = false;
                        btnchoice.Enabled = false;
                        txtbxchoice.Enabled = false;
                    }
                    clientSocket.Close();
                    connected = false;
                }
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Byte[] namebuffer = Encoding.Default.GetBytes(":3:");
                clientSocket.Send(namebuffer);
            }
            catch
            {
                ClientRichTxtBox.AppendText("Cannot Send Disconnection Request!\n");
            }
        }

        private void btnchoice_Click(object sender, EventArgs e)
        {
            try
            {
                int nmbr;
                if (Int32.TryParse(txtbxchoice.Text, out nmbr))
                {
                    if (nmbr >= 0 && nmbr <= 9)
                    {
                        Byte[] choicebuffer = Encoding.Default.GetBytes(":1:" + Convert.ToString(nmbr));
                        clientSocket.Send(choicebuffer);
                    }
                }
                else
                {
                    ClientRichTxtBox.AppendText("Invalid Choice!\n");
                }
            }
            catch
            {
                ClientRichTxtBox.AppendText("Cannot send the choice!\n");
            }
        }
    }
}
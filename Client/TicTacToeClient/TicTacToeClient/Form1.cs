using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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
            string IPadr = txtbxIP.Text;
            IPAddress ipadr;
            if (!IPAddress.TryParse(IPadr, out ipadr))
            {
                ClientRichTxtBox.AppendText("IP not valid!\n");
            }
            else
            {
                int portNum;
                if (Int32.TryParse(txtbxPort.Text, out portNum))
                {
                    if (txtbxName.Text != "")
                    {
                        try
                        {
                            Byte[] name_buffer = Encoding.Default.GetBytes(txtbxName.Text);
                            Byte[] port_buffer = new byte[4096];
                            clientSocket.Connect(ipadr, portNum);

                            clientSocket.Send(name_buffer);

                            clientSocket.Receive(port_buffer);
                            string callback = Encoding.Default.GetString(port_buffer);
                            callback = callback.Replace("\0", "");
                            if (callback.Substring(0, 4) == ":01:")
                            { //Connection Accepted 
                                txtbxIP.Enabled = false;
                                txtbxPort.Enabled = false;
                                btnconnect.Enabled = false;
                                btnconnect.BackColor = Color.Green;
                                btnDisconnect.Enabled = true;
                                txtbxName.Enabled = false;

                                ClientRichTxtBox.Clear();
                                ClientRichTxtBox.AppendText("Connected to the server!\n");
                                connected = true;
                                ClientRichTxtBox.AppendText("Welcome, " + callback.Substring(4) + "!\n");
                                Thread recieveClientThread = new Thread(() => RecieveFromServer());
                                recieveClientThread.Start();
                            }
                            else if (callback.Substring(0, 4) == ":00:")//Connection Denied --Username--
                            {
                                ClientRichTxtBox.AppendText("Username already taken!\n");

                            }
                            else if (callback.Substring(0, 4) == ":02:")
                            { //Connection Denied --Users--
                                ClientRichTxtBox.AppendText("Cannot accept more than 4 users!\n");
                            }
                            else { //Connection Denied
                                ClientRichTxtBox.AppendText("Connection Denied!\n");
                            }
                        }
                        catch
                        {
                            ClientRichTxtBox.AppendText("Cannot connect to the server!\n");
                        }
                    }
                    else
                    {
                        ClientRichTxtBox.AppendText("Username space cannot be left empty!\n");
                    }
                }
                else
                {
                    ClientRichTxtBox.AppendText("Invalid Port!\n");
                }
            }
            ClientRichTxtBox.ScrollToCaret();
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
                    if (messagetype.Substring(0, 2) == ":3")
                    {
                        if (messagetype.Substring(2, 2) == "1:") //Disconnect Success
                        {
                            txtbxIP.Enabled = true;
                            txtbxPort.Enabled = true;
                            btnconnect.Enabled = true;
                            txtbxName.Enabled = true; txtbxName.Clear();


                            btnconnect.BackColor = Control.DefaultBackColor;
                            btnDisconnect.Enabled = false;
                            btnchoice.Enabled = false;
                            txtbxchoice.Enabled = false; txtbxchoice.Clear();
                            ClientRichTxtBox.Clear();

                            connected = false;
                            clientSocket.Close();

                            ClientRichTxtBox.AppendText("Disconnected from the server!\n");
                        }
                        else if (messagetype.Substring(2, 2) == "2:")
                        {
                            ClientRichTxtBox.AppendText(incomingmessage + " has disconnected from the server!\n");
                        }
                        else //Disconnect Fail
                        {
                            ClientRichTxtBox.AppendText("Disconnect Failure!\n");
                        }

                    }
                    else if (messagetype.Substring(0, 2) == ":1") //Choice Type
                    {
                        if (messagetype.Substring(2, 2) == "0:")
                        {
                            ClientRichTxtBox.AppendText(incomingmessage);
                            Byte[] conformation = Encoding.Default.GetBytes(":4:"); //Conformation about wanting to continue game
                            clientSocket.Send(conformation);

                        }
                        else if (messagetype.Substring(2, 2) == "1:")
                        {
                            ClientRichTxtBox.AppendText("<-->Your turn<-->\n");
                            ClientRichTxtBox.AppendText("~~~~~~~\n" + incomingmessage.Substring(0, 1) + "'s turn to play!\n");
                            ClientRichTxtBox.AppendText(incomingmessage.Substring(1) + "~~~~~~~\n");
                            Byte[] conformation = Encoding.Default.GetBytes(":5:"); //Conformation about packet being recieved
                            clientSocket.Send(conformation);
                            txtbxchoice.Enabled = true;
                            btnchoice.Enabled = true;
                        }
                        else if (messagetype.Substring(2, 2) == "2:")
                        {
                            ClientRichTxtBox.AppendText("<-->Other player's turn<-->\n");
                            ClientRichTxtBox.AppendText("~~~~~~~\n" + incomingmessage.Substring(0, 1) + "'s turn to play!\n");
                            ClientRichTxtBox.AppendText(incomingmessage.Substring(1) + "~~~~~~~\n");
                            Byte[] conformation = Encoding.Default.GetBytes(":5:"); //Conformation about packet being recieved
                            clientSocket.Send(conformation);
                            txtbxchoice.Enabled = false;
                            btnchoice.Enabled = false;
                        }
                        else if (messagetype.Substring(2, 2) == "3:")
                        {
                            ClientRichTxtBox.AppendText("~~~~~~~\nChosen cell already chosen, please enter a valid cell!\n");
                            ClientRichTxtBox.AppendText(incomingmessage + "~~~~~~~\n");
                            Byte[] conformation = Encoding.Default.GetBytes(":5:"); //Conformation about packet being recieved
                            clientSocket.Send(conformation);
                            txtbxchoice.Enabled = true;
                            btnchoice.Enabled = true;
                        }
                        else if (messagetype.Substring(2, 2) == "6:") {//Game Reinstated -- Player quit continue position
                            ClientRichTxtBox.AppendText("<------->\nGame Reinstated ~~ A player quit, game continues with new players!\n<------->\n");
                            Byte[] conformation = Encoding.Default.GetBytes(":5:"); //Conformation about packet being recieved
                            clientSocket.Send(conformation);
                        }
                        else
                        {
                            if (messagetype.Substring(2, 2) == "4:")
                            { //Win
                                ClientRichTxtBox.AppendText("<------->\n" + incomingmessage.Substring(1));
                                ClientRichTxtBox.AppendText("Game over! " + incomingmessage.Substring(0, 1) + " wins!\n<------->\n");
                            }
                            else if (messagetype.Substring(2, 2) == "5:")
                            { //Draw
                                ClientRichTxtBox.AppendText("<------->\n" + incomingmessage);
                                ClientRichTxtBox.AppendText("Game over! It's a draw!\n<------->\n");
                            }
                            Byte[] conformation = Encoding.Default.GetBytes(":4:"); //Conformation about wanting to continue game
                            clientSocket.Send(conformation);

                        }
                    }
                    else if (messagetype.Substring(0, 2) == ":6")
                    {
                        txtbxchoice.Clear(); txtbxchoice.Enabled = false;
                        btnchoice.Enabled = false;
                        ClientRichTxtBox.AppendText("The game has been disbanded!\n");
                    }
                    ClientRichTxtBox.ScrollToCaret();
                }
                catch
                {
                    if (!terminating)
                    {
                        ClientRichTxtBox.AppendText("The server has disconnected!\n");
                        ClientRichTxtBox.ScrollToCaret();

                        txtbxIP.Enabled = true;
                        txtbxPort.Enabled = true;
                        btnconnect.Enabled = true;
                        txtbxName.Enabled = true; txtbxName.Clear();

                        btnconnect.BackColor = Control.DefaultBackColor;
                        btnDisconnect.Enabled = false;
                        btnchoice.Enabled = false;
                        txtbxchoice.Enabled = false; txtbxchoice.Clear();
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
            ClientRichTxtBox.ScrollToCaret();
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
                        Array.Clear(choicebuffer);
                    }
                    else
                    {
                        ClientRichTxtBox.AppendText("Out of range choice!\n");
                    }
                }
                else
                {
                    ClientRichTxtBox.AppendText("Invalid Choice!\n");
                }
                txtbxchoice.Clear();
            }
            catch
            {
                ClientRichTxtBox.AppendText("Cannot send the choice!\n");
            }
            ClientRichTxtBox.ScrollToCaret();
        }
    }
}
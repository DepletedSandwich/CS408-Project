using Microsoft.VisualBasic.Logging;
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TicTacToeServer
{
    public partial class Form1 : Form
    {
        /*Client Sockets*/
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IDictionary<string, Socket> Clients = new Dictionary<string, Socket>();

        private static ManualResetEvent choicesignal = new ManualResetEvent(false); //Signal for decision of parties
        private static ManualResetEvent gamecontinuation = new ManualResetEvent(false); //Signal, if both parties want to continue on playing
        private static ManualResetEvent packetchecker = new ManualResetEvent(false); //Signal, package dialogue has to be disciplined
        private static Mutex incgame = new Mutex();
        /*Client Sockets*/
        
        /*Network Var*/
        bool terminate = false, listen = false;
        /*Network Var*/

        /*ServerGameBoard*/
        struct boardcell
        {
            public string dispstr;
            public bool used;
        }


        int choice = 0; int continuegame = 0;

        private static string showBoard(ref boardcell[,] board)
        {
            //Show Board to User
            string strboard = "";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1) - 1; j++)
                {
                    strboard += board[i, j].dispstr + " | ";
                }
                strboard += board[i, 2].dispstr + "\n";
            }
            return strboard;
        }
        private static bool checkgame(ref boardcell[,] gameBoard, ref string cond)
        {
            //Check for rows
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                if (gameBoard[i, 0].dispstr == gameBoard[i, 1].dispstr && gameBoard[i, 1].dispstr == gameBoard[i, 2].dispstr)
                {
                    cond = gameBoard[i, 0].dispstr;
                    return false;
                }
            }


            //Check for columns
            for (int j = 0; j < gameBoard.GetLength(1); j++)
            {
                if (gameBoard[0, j].dispstr == gameBoard[1, j].dispstr && gameBoard[1, j].dispstr == gameBoard[2, j].dispstr)
                {
                    cond = gameBoard[0, j].dispstr;
                    return false;
                }
            }

            //Check for diagonals
            if ((gameBoard[0, 0].dispstr == gameBoard[1, 1].dispstr && gameBoard[1, 1].dispstr == gameBoard[2, 2].dispstr) ||
                (gameBoard[0, 2].dispstr == gameBoard[1, 1].dispstr && gameBoard[1, 1].dispstr == gameBoard[2, 0].dispstr))
            {
                cond = gameBoard[0, 0].dispstr;
                return false;
            }
            int counter = 0;
            //Check for draw
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j].used == true)
                    {
                        counter++;
                    }
                }
            }
            if (counter == gameBoard.Length)
            {
                cond = "draw";
                return false;
            }
            //Continue
            return true;
        }

        private static string actionfillboard(ref boardcell[,] board, int action, string playerinfo)
        {
            if (board[(int)(action - 1) / 3, (action - 1) % 3].used == false)
            {
                board[(int)(action - 1) / 3, (action - 1) % 3].dispstr = playerinfo;
                board[(int)(action - 1) / 3, (action - 1) % 3].used = true;
                return "success";
            }
            else
            {
                return "failure";
            }
        }
        /*ServerGameBoard*/


        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form_Close);
            InitializeComponent();
        }
        private void Form_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listen = false;
            terminate = true;
            Environment.Exit(0);
        }

        private void btnlisten_Click(object sender, EventArgs e)
        {
            if (txtbxport.Text != "")
            {
                int serverPort;
                if (Int32.TryParse(txtbxport.Text, out serverPort))
                {
                    IPEndPoint ServerPoint = new IPEndPoint(IPAddress.Any, serverPort);
                    serverSocket.Bind(ServerPoint);
                    serverSocket.Listen(3);

                    btnlisten.Enabled = false;
                    listen = true;
                    btnlisten.BackColor = Color.Green;

                    Thread thread_accept = new Thread(Accept);
                    thread_accept.Start();

                    txtinfogame.AppendText("Server started listeing on port: " + serverPort + "\n");
                    txtbxport.Enabled = false;
                }
                else
                {
                    txtinfogame.AppendText("Please Check Input Port!\n");
                    txtbxport.Clear();
                }
            }
            else
            {
                txtinfogame.AppendText("Please Enter Port!\n");
                txtbxport.Clear();
            }
        }

        private void Accept()
        {
            while (listen)
            {
                try
                {
                    Byte[] name_buffer = new Byte[4096];
                    Socket newClient = serverSocket.Accept();
                    //Ask for Name
                    newClient.Receive(name_buffer);
                    string potname = Encoding.Default.GetString(name_buffer);
                    potname = potname.Replace("\0", "");

                    if (!Clients.ContainsKey(potname)) //Name doesn't exist accept connection
                    {
                        Clients.Add(potname, newClient);
                        Thread listenerThread = new Thread(() => ThreadListen(newClient));
                        listenerThread.Start();
                        txtinfogame.AppendText(potname + " connected to the server!\n");
                        Byte[] port_buffer = Encoding.Default.GetBytes(":01:" + potname);//Success
                        newClient.Send(port_buffer);


                    }
                    else //If not then break it
                    {
                        Byte[] port_buffer = Encoding.Default.GetBytes(":00:");
                        newClient.Send(port_buffer);
                        newClient.Close();
                    }
                    if (Clients.Count == 2) btnstartgame.Enabled = true; //Game Can be Started 
                    else btnstartgame.Enabled = false; //Game cannot start

                }
                catch
                {
                    if (terminate)
                    {
                        listen = false;
                    }
                    else
                    {
                        txtinfogame.AppendText("The socket stopped working.\n");
                    }
                }
            }
        }

        private void ThreadListen(Socket thisClient)
        {
            bool connected = true;
            Byte[] messagesender;
            while (connected && !terminate)
            {
                try
                {
                    Byte[] listen_buffer = new Byte[256];
                    thisClient.Receive(listen_buffer);

                    string incoming_message = Encoding.Default.GetString(listen_buffer);
                    incoming_message = incoming_message.Replace("\0", "");

                    string message_type = incoming_message.Substring(0, 3);
                    incoming_message = incoming_message.Substring(3);

                    if (message_type == ":3:") //Disconnect
                    {
                        incgame.WaitOne();
                        choice = -1; //Terminate the game when choice -1
                        choicesignal.Set();
                        incgame.ReleaseMutex();
                        messagesender = Encoding.Default.GetBytes(":31:");//Disconnect Success
                        thisClient.Send(messagesender);
                        thisClient.Close();
                        string inf = "";
                        foreach (string item in Clients.Keys)
                        {
                            if (Clients[item] == thisClient)
                            {
                                inf = item;
                                Clients.Remove(item);
                                txtinfogame.AppendText(item + " has disconnected!\n");
                            }
                        }
                        foreach (string item in Clients.Keys) //Send Other Clients Information
                        {
                            messagesender = messagesender = Encoding.Default.GetBytes(":32:" + inf); //Other Clients Know that a client disconnected
                            Clients[item].Send(messagesender);
                            Array.Clear(messagesender);
                        }

                        connected = false;
                        if (Clients.Count == 2) btnstartgame.Enabled = true; //Game Can be Started 
                        else btnstartgame.Enabled = false; //Game cannot start
                    }
                    else if (message_type == ":1:") //Choice Incoming
                    {
                        incgame.WaitOne();
                        choice = Convert.ToInt32(incoming_message);
                        choicesignal.Set();
                        incgame.ReleaseMutex();
                    }
                    else if (message_type == ":4:") //Game Continue
                    {
                        incgame.WaitOne();
                        if (continuegame == 1)
                        {
                            continuegame = 0;
                            gamecontinuation.Set();
                        }
                        else
                        {
                            continuegame++;
                        }
                        incgame.ReleaseMutex();
                    }
                    else if (message_type == ":5:")
                    {
                        incgame.WaitOne();
                        packetchecker.Set(); // Client has sent a conformation about the package recievement
                        incgame.ReleaseMutex();
                    }

                }
                catch
                {
                    incgame.WaitOne();
                    choice = -1; //Terminate the game when choice -1
                    choicesignal.Set();
                    incgame.ReleaseMutex();
                    thisClient.Close();
                    string inf = "";
                    foreach (string item in Clients.Keys)
                    {
                        if (Clients[item] == thisClient)
                        {
                            inf = item;
                            Clients.Remove(item);
                            txtinfogame.AppendText(item + " has disconnected!\n");
                        }
                    }
                    foreach (string item in Clients.Keys)
                    {
                        messagesender = Encoding.Default.GetBytes(":32:" + inf);
                        Clients[item].Send(messagesender);
                        Array.Clear(messagesender);
                    }
                    connected = false;
                    if (Clients.Count == 2) btnstartgame.Enabled = true; //Game Can be Started 
                    else btnstartgame.Enabled = false; //Game cannot start
                }
            }
        }

        private void StartGame()
        {
            incgame.WaitOne();
            choice = 0;
            choicesignal.Reset();
            incgame.ReleaseMutex();
            bool gamecont = true; //If a player disconnects that game has to be stopped
            double[] playerscores = { 0, 0 };
            List<string> players = Clients.Keys.ToList();
            txtinfogame.AppendText($"Game between {players[0]} and {players[1]} started!\n");
            try
            {
                while (gamecont)
                {
                    Byte[] gameinfobuffer;
                    string gameinfo = $":10:--------------\nGame Starts | {players[0]} (X player): {playerscores[0]} vs. {players[1]} (O player): {playerscores[1]}\n--------------\n";
                    /*Send both parties conformation of game start*/

                    foreach (string item in Clients.Keys)
                    {
                        gameinfobuffer = Encoding.Default.GetBytes(gameinfo);
                        Clients[item].Send(gameinfobuffer);
                        Array.Clear(gameinfobuffer);
                    }
                    gamecontinuation.WaitOne();
                    gamecontinuation.Reset();
                    boardcell[,] board = new boardcell[3, 3];
                    bool startgame = true;
                    int turn = 0;

                    //Fill the board with indexes
                    int indx = 1;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            board[i, j].dispstr = indx.ToString();
                            board[i, j].used = false;
                            indx++;
                        }
                    }

                    while (startgame)
                    {
                        string? currentplayer = turn == 0 ? "X" : "O";
                        string res = "continue";

                        foreach (string item in players)
                        {
                            if (players[turn] == item) //Its this players turn
                            {
                                gameinfobuffer = Encoding.Default.GetBytes(":11:" + currentplayer + showBoard(ref board));
                                Clients[item].Send(gameinfobuffer);
                                packetchecker.WaitOne();
                                incgame.WaitOne();
                                packetchecker.Reset();
                                incgame.ReleaseMutex();
                            }
                            else
                            {
                                gameinfobuffer = Encoding.Default.GetBytes(":12:" + currentplayer + showBoard(ref board));
                                Clients[item].Send(gameinfobuffer);
                                packetchecker.WaitOne();
                                incgame.WaitOne();
                                packetchecker.Reset();
                                incgame.ReleaseMutex();
                            }
                            Array.Clear(gameinfobuffer);
                        }
                        choicesignal.WaitOne();
                        incgame.WaitOne();
                        choicesignal.Reset();
                        if (choice == -1)
                        {
                            choice = 0;
                            incgame.ReleaseMutex();
                            throw new Exception();
                        }
                        else
                        {
                            incgame.ReleaseMutex();
                            while (actionfillboard(ref board, choice, currentplayer) == "failure")
                            {
                                gameinfobuffer = Encoding.Default.GetBytes(":13:" + showBoard(ref board)); //Error
                                Clients[players[turn]].Send(gameinfobuffer);

                                packetchecker.WaitOne();
                                incgame.WaitOne();
                                    packetchecker.Reset();
                                incgame.ReleaseMutex();
                                Array.Clear(gameinfobuffer);
                                choicesignal.WaitOne();
                                incgame.WaitOne();
                                    choicesignal.Reset();
                                if (choice == -1)
                                {
                                    choice = 0;
                                    incgame.ReleaseMutex();
                                    throw new Exception();
                                }
                                incgame.ReleaseMutex();
                            }

                            startgame = checkgame(ref board, ref res); /*Checking whoever won*/
                            if (res != "continue")
                            {
                                if (res != "draw")
                                {
                                    playerscores[turn] += 1;
                                    /*Send Result to both parties*/
                                    foreach (string item in players)
                                    {
                                        gameinfobuffer = Encoding.Default.GetBytes(":14:" + currentplayer + showBoard(ref board));
                                        Clients[item].Send(gameinfobuffer);
                                        Array.Clear(gameinfobuffer);
                                    }
                                }
                                else //Draw
                                {
                                    playerscores[0] += 0.5; playerscores[1] += 0.5;
                                    foreach (string item in players)
                                    {
                                        gameinfobuffer = Encoding.Default.GetBytes(":15:" + showBoard(ref board));
                                        Clients[item].Send(gameinfobuffer);
                                        Array.Clear(gameinfobuffer);
                                    }
                                }
                            }
                            turn = turn == 0 ? 1 : 0;
                        }
                    }
                    gamecontinuation.WaitOne();
                    incgame.WaitOne();
                    gamecontinuation.Reset();
                    incgame.ReleaseMutex();
                    players.Reverse();
                    Array.Reverse(playerscores);
                }
            }
            catch //Specific client disconnection
            {
                txtinfogame.AppendText($"The game between {players[0]} and {players[1]} has been disbanded!\n");
                /*First Check Connectivity of clients*/
                foreach (string item in Clients.Keys)
                {
                    if (Clients[item].Connected != false) /*Send this client info about game disband*/
                    {
                        Byte[] disband_message = Encoding.Default.GetBytes(":61:"); //Disband Error is sent to active clients :61:
                        Clients[item].Send(disband_message);
                        Array.Clear(disband_message);
                    }
                }
                incgame.WaitOne();
                choice = 0;
                incgame.ReleaseMutex();
                if (Clients.Count == 2) btnstartgame.Enabled = true; //Game Can be Started 
                else btnstartgame.Enabled = false; //Game cannot start

            }

        }

        private void btnstartgame_Click(object sender, EventArgs e)
        { //Start the game when button is clicked
            btnstartgame.Enabled = false;
            Thread gameStarter = new Thread(() => StartGame());
            gameStarter.Start();

        }
    }
}
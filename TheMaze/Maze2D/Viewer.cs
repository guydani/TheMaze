using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Server.Interface;


namespace Server.Maze2D
{
    /* the acceptable commands for this server is:
       for create single player:
            1. generate1 [name] [sizeHight] [sizeWidth]
            2. generate2 [name] [sizeHight] [sizeWidth]
            3. generate3 [name] [sizeHight] [sizeWidth]
        for find solution from one point to the end:
            1. solve1 [name] [pointHeight] [pointWidth]
            2. solve2 [name] [pointHeight] [pointWidth]
        for getting the multi player waiting for play:
            games_waiting
        for ask to play in multiplayer mod:
            multiplayer [name] [sizeHight] [sizeWidth]
        for ask to move to direction in multiplayer mod:
            move [name] [direction = up, down, left, right] [index]
            importent: this index is as we want it to get to the other player. if one index miss - problem.
            the index strats from 1.
        */
    public class Viewer : IViewer
    {
        public event MessageRecived MessageRecivedWaitToExecute;
        public Dictionary<int, IClient> ClientSaver { get; }
        private Socket socket;

        public Viewer()
        {
            ClientSaver = new Dictionary<int, IClient>();
            string port = ConfigurationManager.AppSettings["PORT_NUMBER"];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, Int32.Parse(port));
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipep);
            socket.Listen(10);
        }

        public void Start()
        {
            while (true)
            {
                Socket c = socket.Accept();
                Client client = new Client(c);
                bool isActive = true;
                Thread t = new Thread(delegate ()
                {
                    while (isActive)
                    {
                        byte[] data = new byte[1024];
                        int recv = c.Receive(data);
                        if (recv == 0) break;
                        string str = Encoding.ASCII.GetString(data, 0, recv);
                        String[] commands = str.Split(new[] { "\n" }, StringSplitOptions.None);
                        foreach (var i in commands)
                        {
                            if (i == "") { break; }
                            if(i == "exit") { isActive = false; }
                            OnMessageRecived(i, client);
                        }
                    }
                    c.Close();
                });
                t.Start();
            }

        }

        public void SendMessage(string json, int index)
        {
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(json.ToUpper());
            IClient client = ClientSaver[index];
            ClientSaver.Remove(index);
            client.Socket.Send(data, data.Length * sizeof(byte), SocketFlags.None);
        }

        protected virtual void OnMessageRecived(string s, IClient client)
        {
            if (MessageRecivedWaitToExecute != null)
                MessageRecivedWaitToExecute(s, client);
        }

        public void AddClientSaver(int index, IClient client)
        {
            ClientSaver[index] = client;
        }
    }
}

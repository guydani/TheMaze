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
    public class Viewer: IViewer
    {
        public event MessageRecived MessageRecivedWaitToExecute;
        public Dictionary<int, IClient> ClientSaver { get;}
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
                Thread t = new Thread(delegate () {
                    while (true)
                    {
                        byte[] data = new byte[1024];
                        int recv = c.Receive(data);
                        if (recv == 0) break;
                        string str = Encoding.ASCII.GetString(data, 0, recv);
                        String[] commands = str.Split(new[] { "\n" },StringSplitOptions.None);
                        foreach(var i in commands)
                        {
                            if (i == "") { break; }
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

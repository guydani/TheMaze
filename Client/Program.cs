using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string configvalue1 = ConfigurationManager.AppSettings["SERVER_IP"];
            string configvalue2 = ConfigurationManager.AppSettings["SERVER_PORT_NUMBER"];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(configvalue1), Int32.Parse(configvalue2));
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                Thread thread = new Thread(delegate ()
                {
                    while (true)
                    {
                        byte[] data = new byte[1024];
                        int recv = server.Receive(data);
                        string stringData = Encoding.ASCII.GetString(data, 0, recv);
                        Console.WriteLine(stringData);
                    }
                });
                thread.Start();
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input == "exit") break;
                    server.Send(Encoding.ASCII.GetBytes(input + "\n"));
                }
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            catch (SocketException e) { Console.WriteLine("Unable to connect to server." + e.ToString()); }

        }
    }

}


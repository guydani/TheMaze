using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Server.Interface;

namespace Server.Maze2D
{
    public class Client : IClient
    {
        public Socket Socket { get; set; }

        public Client(Socket s)
        {
            Socket = s;
        }
    }
}

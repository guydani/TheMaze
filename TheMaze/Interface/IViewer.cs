using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Interface;
using Server.Maze2D;

namespace Server
{
    public delegate void MessageRecived(string data, IClient client);
    public interface IViewer
    {
        event MessageRecived MessageRecivedWaitToExecute;
        Dictionary<int, IClient> ClientSaver { get; }
        void Start();
        void SendMessage(string json, int index);
        void AddClientSaver(int index, IClient client);
    }
}

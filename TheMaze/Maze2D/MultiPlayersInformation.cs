using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Interface;

namespace Server.Maze2D
{
    public class MultiPlayersInformation
    {
        public string Name { get; set; }
        public IClient FirstClient { get; set; }
        public int Index1 { get; set; }
        public IClient SecondClient { get; set; }
        public int Index2 { get; set; }


        public MultiPlayersInformation(string n, IClient client, int i)
        {
            Name = n;
            FirstClient = client;
            SecondClient = null;
            Index1 = i;
        }

        public void AddInformation(IClient client, int i)
        {
            SecondClient = client;
            Index2 = i;
        }

        public bool isFullGame()
        {
            if (SecondClient != null)
            {
                return true;
            }
            return false;
        }
    }
}

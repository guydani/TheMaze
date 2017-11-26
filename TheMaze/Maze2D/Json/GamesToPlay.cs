using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Maze2D.Json
{
    class GamesToPlay: AbstractJson
    {
        public GamesToPlay() {
            GameWaiting = new List<string>();
        }
        public List<string> GameWaiting { get; set; }

        public void AddAnotherGame(string s)
        {
            GameWaiting.Add(s);
        }
    }
}

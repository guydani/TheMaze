using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Maze2D.Json
{
    public class MazePropertyMultiPlayer: MazeProperty
    {
        public List<int> AdditionalStartPoint { get; set; }

        public MazePropertyMultiPlayer() : base()
        {
        }
    }
}

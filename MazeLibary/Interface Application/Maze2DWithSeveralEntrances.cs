using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.Interface_Application
{
    public class Maze2DWithSeveralEntrances: Maze2D
    {
        public int SeveralEntraces { set; get; }
        public Maze2DWithSeveralEntrances(List<int> b, int i) : base(b)
        {
        }
    }
}

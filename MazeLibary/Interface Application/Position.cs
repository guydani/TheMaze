using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;

namespace MazeLibary.Interface_Application
{
    public class Position: IPosition<int>
    {
        private List<int> position;
        private bool wall;

        public Position(List<int> p, bool w)
        {
            position = p;
            wall = w;
        }

        public List<int> GetPosition
        {
            get { return position; }
            set { position = value; }
        }
        public bool IsWall
        {
            get { return wall; }
            set { wall = value; }
        }
    }
}

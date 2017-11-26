using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;

namespace MazeLibary.Interface_Application
{
    public class Cell: ICell<int>
    {
        private List<int> position;
        private bool isVisited;
        private bool wall;
        private int cost = 1;
        private List<ICell<int>> neighbors;
        public ICell<int> CameFrom { get; set; }

        public Cell(List<int> p, bool w, List<ICell<int>> n)
        {
            position = p;
            wall = w;
            neighbors = n;
            CameFrom = null;
            isVisited = false;
        }

        public List<int> GetPosition
        { get { return position; }
          set { position = value; } }
        public bool IsWall
        { get { return wall; }
          set { wall = value; } }
        public int Cost
        { get { return cost; }
          set { cost = value; } }
        public List<ICell<int>> Neighbors
        { get { return neighbors; }
          set { neighbors = value; } }

        public bool IsVisited
        {
            get { return isVisited; }
            set { isVisited = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || ((Cell) obj).GetPosition[0] != GetPosition[0]
                                || ((Cell) obj).GetPosition[1] != GetPosition[1])
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return position[0].GetHashCode() ^ position[1].GetHashCode();
        }
    }
}

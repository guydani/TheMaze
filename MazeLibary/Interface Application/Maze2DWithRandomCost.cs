using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;

namespace MazeLibary.Interface_Application
{
    public class Maze2DWithRandomCost: Maze2D
    {
        public Maze2DWithRandomCost(List<int> b) : base(b)
        {
        }

        public override void SetCostForCellsInMaze()
        {
            Random random = new Random();
            foreach (var i in maze)
            {
                if (i is Cell)
                {
                    ((ICell<int>)i).Cost = random.Next(1, 10);
                }
                ((Cell) maze[Entrance.GetPosition[0], Entrance.GetPosition[1]]).Cost = 0;
                ((Cell) maze[Exit.GetPosition[0], Exit.GetPosition[1]]).Cost = 0;
            }
        }
    }
}

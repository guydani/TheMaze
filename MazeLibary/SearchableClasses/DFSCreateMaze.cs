using MazeLibary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.SearchableClasses
{
    /* creates the maze by using dfs, as the psadu code in wikipedia. 
     * most the time this maze is simplier then others */
    public class DFSCreateMaze<ICell, T, IPosition> : AbstractCreateMaze<ICell, T, IPosition> where T : IComparable
    {
        public override void CreateMaze()
        {
            Stack<ICell<T>> stack = new Stack<ICell<T>>(); 
            Random random = new Random();
            ICell<T> currentCell = Maze.Entrance;
            ICell<T> newCell;
            currentCell.IsVisited = true;
            currentCell.IsWall = false;
            while(CheckIfUnVisitedCell() || stack.Count == 0)
            {
                var l = Maze.GetListOfWalls(currentCell);
                if (l.Exists(x => x.IsWall && 
                        Maze.GetWhichCellsThisWallSeperate(x).Exists(item => !item.IsVisited)))
                {
                    newCell = GetNewCell(l);
                    stack.Push(newCell);
                    Maze.BreakWallBetweenTwoCells(currentCell, newCell);
                    newCell.IsVisited = true;
                    newCell.IsWall = false;
                    currentCell = newCell;
                } else if(stack.Count > 0)
                {
                    currentCell = stack.Pop();
                }
            }
        }

        private ICell<T> GetNewCell(List<IPosition<T>> l)
        {
            Random random = new Random();
            int temp;
            List<ICell<T>> list = new List<ICell<T>>();
            foreach (var x in l)
            {
                foreach (var item in Maze.GetWhichCellsThisWallSeperate(x))
                {
                    if (!item.IsVisited)
                    {
                        list.Add(item);
                    }
                }
            }
            temp = random.Next(0, list.Count);
            return list[temp];
        }
    }
}

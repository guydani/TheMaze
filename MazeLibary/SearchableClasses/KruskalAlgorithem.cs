using MazeLibary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.SearchableClasses
{
    public class KruskalAlgorithem<ICell, T, IPosition> : AbstractCreateMaze<ICell, T, IPosition> where T : IComparable
    {
        public override void CreateMaze()
        {
            List<List<ICell<T>>> l = new List<List<ICell<T>>>();
            List<IPosition<T>> listOfWalls = Maze.GetListOfAllWalls();
            Random random = new Random();
            foreach(ICell<T> i in Maze)
            {
                i.IsWall = false;
                List<ICell<T>> list = new List<ICell<T>>();
                list.Add(i);
                l.Add(list);
            }
            while(listOfWalls.Count > 0)
            {
                bool breakWall = true;
                int i = listOfWalls.Count;
                i = random.Next(0, i);
                IPosition<T> wall = listOfWalls[i];
                List<ICell<T>> cells = Maze.GetWhichCellsThisWallSeperate(wall);
                List<ICell<T>> first = null;
                List<ICell<T>> second = null;
                foreach (var listOfCells in l)
                {
                    if (listOfCells.Contains(cells[0]) &&
                            listOfCells.Contains(cells[1]))
                    {
                        listOfWalls.Remove(wall);
                        breakWall = false;
                        break;
                    }
                    if ((listOfCells.Contains(cells[0]) || listOfCells.Contains(cells[1]))
                        && first == null)
                    {
                        first = listOfCells;
                    }else if ((listOfCells.Contains(cells[0]) || listOfCells.Contains(cells[1]))
                        && second == null)
                    {
                        second = listOfCells;
                    } 
                }
                if (breakWall)
                {
                    Maze.BreakWallBetweenTwoCells(cells[0], cells[1]);
                    l.Remove(first);
                    l.Remove(second);
                    second.ForEach(item => first.Add(item));
                    l.Add(first);
                }
            }
        }
    }
}

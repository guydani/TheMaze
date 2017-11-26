using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.SearchableClasses
{
    public class PrimsAlgorithem<ICell, T, IPosition> : AbstractCreateMaze<ICell, T, IPosition> where T : IComparable
    {
        public override void CreateMaze()
        {
            var cell = Maze.Entrance;
            cell = Maze.GetCellInPosition(cell);
            cell.IsVisited = true;
            Random random = new Random();
            var listWalls = Maze.GetListOfWalls(cell);
            while (listWalls.Count > 0)
            {
                var randomInteger = random.Next(0, listWalls.Count);
                var wall = listWalls[randomInteger];
                var cells = Maze.GetWhichCellsThisWallSeperate(wall);
                if ((cells[0].IsVisited && !cells[1].IsVisited) || 
                        (!cells[0].IsVisited && cells[1].IsVisited))
                {
                    cells[0].IsWall = false;
                    cells[1].IsWall = false;
                    cells[0].IsVisited = true;
                    cells[1].IsVisited = true;
                    wall.IsWall = false;
                    listWalls.Remove(wall);
                    var l = Maze.GetListOfWalls(cells[0]);
                    Maze.GetListOfWalls(cells[1]).ForEach(item => l.Add(item));
                    l.ForEach(x => listWalls.Add(x));
                    listWalls.Select(x => x.GetPosition).Distinct();
                }
                else
                {
                    listWalls.Remove(wall);
                }
            }
        }

    }
}

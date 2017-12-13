using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.Interfaces
{
    /*
        the icreator, is responsible on create the maze, and initial all positions
    */
    public interface ICreator<ICell, T, IPosition> where T : IComparable
    {
        IMaze<ICell<T>, T, IPosition<T>> Maze { get; set; }
        void InitialMaze();
        void CreateMaze();
    }
}

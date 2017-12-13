using System.Collections.Generic;
using System;

namespace MazeLibary.Interfaces
{
    /* 
        the cell is a position in the grid, but it not symbol a wall.
        in some mazes, walk on a cell cost.
        cell's can be only in even places.
    */
    public interface ICell<T>: IPosition<T> where T : IComparable
    {
        int Cost { get; set; }
        List<ICell<T>> Neighbors { get; set; }
        bool IsVisited { get; set; }
        ICell<T> CameFrom { get; set; }
    }
}

using System.Collections.Generic;
using System;

namespace MazeLibary.Interfaces
{
    public interface ICell<T>: IPosition<T> where T : IComparable
    {
        int Cost { get; set; }
        List<ICell<T>> Neighbors { get; set; }
        bool IsVisited { get; set; }
        ICell<T> CameFrom { get; set; }
    }
}

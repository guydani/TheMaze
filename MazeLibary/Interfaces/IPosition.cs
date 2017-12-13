using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.Interfaces
{
    /*
        this is the smallest piece of the maze. we use list, because we want to
        expand our project to sevral dimentions.
        we use position for represent also walls that in odd places.
        */
    public interface IPosition<T> where T : IComparable
    {
        List<T> GetPosition { get; set; }
        bool IsWall { get; set; }
    }
}

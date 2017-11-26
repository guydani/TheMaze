using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.Interfaces
{
    public interface IPosition<T> where T : IComparable
    {
        List<T> GetPosition { get; set; }
        bool IsWall { get; set; }
    }
}

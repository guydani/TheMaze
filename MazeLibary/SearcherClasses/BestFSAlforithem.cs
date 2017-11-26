using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;

namespace MazeLibary.SearcherClasses
{
    public class BestFSAlforithem<T>: AbstractSearcher<T> where T: IComparable
    {
        public BestFSAlforithem() : base()
        {
            openList = new MyPriorityQueue<ICell<T>, T>();
        }
    }
}

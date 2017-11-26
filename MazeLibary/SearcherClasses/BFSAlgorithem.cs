using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;

namespace MazeLibary.SearcherClasses
{
    public class BFSAlgorithem<T>: AbstractSearcher<T> where T: IComparable
    {
        public BFSAlgorithem() : base()
        {
            openList = new MyQueue<ICell<T>, T>();
        }
    }
}

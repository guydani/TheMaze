using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;

namespace MazeLibary.SearcherClasses
{
    public class MyPriorityQueue<ICell, T> :MyQueue<ICell, T> where T : IComparable
    {
        public override ICell<T> Dequeue()
        {
            var min = queue.First.Value;
            foreach (var i in queue)
            {
                if (min.Cost > i.Cost)
                {
                    min = i;
                }
            }
            this.queue.Remove(min);
            return min;
        }
    }
}

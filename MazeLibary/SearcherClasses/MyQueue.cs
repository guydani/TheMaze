using System;
using System.Collections.Generic;
using System.Linq;
using MazeLibary.Interfaces;

namespace MazeLibary.SearcherClasses
{
    public class MyQueue<ICell, T> where T : IComparable
    {
        protected LinkedList<ICell<T>> queue;

        public MyQueue()
        {
            queue = new LinkedList<ICell<T>>();
        }

        public virtual ICell<T> Dequeue()
        {
            var v = queue.First();
            queue.Remove(v);
            return v;
        }

        public void Enqueue(ICell<T> cell)
        {
            this.queue.AddLast(cell);
        }

        public bool isEmpty()
        {
            if (queue.Count() == 0)
                return true;
            return false;
        }

        public bool isContain(ICell<T> s)
        {
            if (queue.Contains(s))
                return true;
            return false;
        }

        public ICell<T> ContainRet(ICell<T> s)
        {
            return queue.Find(s).Value;
        }
    }
}

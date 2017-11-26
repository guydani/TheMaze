using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server.Maze2D.Tasks;

namespace Server.Maze2D
{
    public class QueueMoveTask
    {
        private LinkedList<MoveTask> queue;
        private Mutex mutex;
        private volatile bool isActive;
        private Thread thread;

        public QueueMoveTask()
        {
            queue = new LinkedList<MoveTask>();
            mutex = new Mutex();
            isActive = false;
            thread = null;
        }

        public void Dequeue()
        {
            var v = queue.First();
            queue.Remove(v);
            v.HandleTask();
        }

        public void Enqueue(MoveTask moveTask)
        {
            queue.AddLast(moveTask);
            mutex.WaitOne();
            if (!isActive)
            {
                isActive = true;
                thread = new Thread(() => Dequeue());
            } 
            mutex.ReleaseMutex();
        }

        public bool isEmpty()
        {
            if (queue.Count == 0)
                return true;
            return false;
        }

        public void ContinueActiveQueue()
        {
            if (isEmpty())
            {
                thread.Join();
                isActive = false;

            }
            else
            {
                Dequeue();
            }
        }
    }
}

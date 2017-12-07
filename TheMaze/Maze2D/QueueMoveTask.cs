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
        private SemaphoreSlim semaphoreSlim;
        private List<Thread> lThread;

        public QueueMoveTask()
        {
            queue = new LinkedList<MoveTask>();
            semaphoreSlim = new SemaphoreSlim(0);
            lThread = new List<Thread>();
            for(int i = 0; i < 5; i++)
            {
                lThread.Add(new Thread(() => Dequeue()));
                lThread[i].Start();
            }
        }

        public void Dequeue()
        {
            while(true)
            {
                semaphoreSlim.Wait();
                var v = queue.First();
                queue.Remove(v);
                v.HandleTask();
            }
        }

        public void Enqueue(MoveTask moveTask)
        {
            queue.AddLast(moveTask);
            semaphoreSlim.Release();
        }
    }
}

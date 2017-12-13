using Server.Maze2D.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class PriorityQueue
    {
        private List<MoveTask> queue;
        private int startNumber;
        private SemaphoreSlim semaphore;
        private Thread thread;

        public PriorityQueue(int start)
        {
            queue = new List<MoveTask>();
            startNumber = start;
            semaphore = new SemaphoreSlim(0);
            thread = new Thread(() => LoopPriorityQueue());
            thread.Start();
        }

        public bool CheckIndex()
        {
            foreach(var i in queue)
            {
                if(i.IndexInClient == startNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public void Enqueue(MoveTask moveTask)
        {
            queue.Add(moveTask);
            Console.WriteLine("Enqueue " + moveTask.Name + " " + moveTask.Direction + "\n");
            semaphore.Release();
        }

        private void LoopPriorityQueue()
        {
            while (true)
            {
                semaphore.Wait();
                while(!isEmpty() && CheckIndex())
                {
                    for (int i = 0; i < queue.Count; i++)
                    {
                        var temp = queue.ElementAt(i);
                        if (temp.IndexInClient == startNumber)
                        {
                            startNumber += 1;
                            temp.HandleTask();
                            Console.WriteLine("Dequeue " + temp.Name+ " " + temp.Direction + "\n");
                            queue.Remove(temp);
                            break;
                        }
                    }
                }
            }
        }

        private bool isEmpty()
        {
            if (queue.Count() == 0)
                return true;
            return false;
        }

    }
}

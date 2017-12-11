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
        private LinkedList<MoveTask> queue;
        private int startNumber;
        private TaskFactory taskFactory;

        public PriorityQueue(int start)
        {
            queue = new LinkedList<MoveTask>();
            startNumber = start;
            taskFactory = new TaskFactory();
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
            if (moveTask.IndexInClient != startNumber)
            {
                return;
            }
            startNumber += 1;
            taskFactory.StartNew(moveTask.HandleTask);
            while(!isEmpty() && CheckIndex())
            {
                for(int i = 0; i < queue.Count; i++)
                {
                    var temp = queue.ElementAt(i);
                    if (temp.IndexInClient == startNumber)
                    {
                        startNumber += 1;
                        taskFactory.StartNew(temp.HandleTask);
                        queue.Remove(temp);
                        break;
                    }
                }
            }
        }

        public bool isEmpty()
        {
            if (queue.Count() == 0)
                return true;
            return false;
        }

    }
}

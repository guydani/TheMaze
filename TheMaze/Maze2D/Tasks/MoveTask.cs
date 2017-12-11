using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Interface;

namespace Server.Maze2D.Tasks
{
    public class MoveTask: ITask
    {
        public string Name { get; set; }
        public string Direction { get; set; }
        public int IndexInClient { get; set; }
        public int NumberOfTask { get; set; }
        private IModel model;

        public MoveTask(IModel m)
        {
            model = m;
        }
        public void SetCommand(string[] s, int index)
        {
            try
            {
                Name = s[1];
                Direction = s[2];
                IndexInClient = int.Parse(s[3]);
                NumberOfTask = index;
            }
            catch (Exception)
            {
                throw new Exception("problem with command");
            }
        }

        public void HandleTask()
        {
            model.MultiPlayerMove(Direction, NumberOfTask);
        }
    }
}

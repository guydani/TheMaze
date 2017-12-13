using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Interface;

namespace Server.Maze2D.Tasks
{
    class CloseTask : ITask

    {
        public int NumberOfTask { get; set; }
        public IModel model;
        public string Name { get; set; }

        public CloseTask(IModel m)
        {
            model = m;
        }
        public void SetCommand(string[] s, int index)
        {
            NumberOfTask = index;
            Name = s[1];
        }

        public void HandleTask()
        {
            model.CloseOption(Name, NumberOfTask);
        }
    }
}

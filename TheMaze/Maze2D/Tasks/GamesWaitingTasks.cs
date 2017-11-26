using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Interface;

namespace Server.Maze2D.Tasks
{
    class GamesWaitingTasks: ITask
    {
        public int NumberOfTask { get; set; }
        private IModel model;

        public GamesWaitingTasks(IModel m)
        {
            model = m;
        }

        public void SetCommand(string[] s, int index)
        {
            NumberOfTask = index;
        }

        public void HandleTask()
        {
            model.GetMultiPlayersWaiting(NumberOfTask);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Interface;

namespace Server.Maze2D.Tasks
{
    public class MultiPlayerTask : ITask
    {
        public string Name { get; set; }
        public int NumberOfTask { get; set; }
        public IClient Client { get; set; }
        public List<int> size;
        private IModel model;

        public MultiPlayerTask(IModel m)
        {
            model = m;
        }

        public void SetCommand(string[] s, int index)
        {
            try { 
                Name = s[1];
                size = new List<int>();
                size.Add(Int32.Parse(s[2]));
                size.Add(Int32.Parse(s[3]));
                NumberOfTask = index;
            }
            catch (Exception)
            {
                throw new Exception("problem with command");
            }
        }

        public void SetMoreDetailes(IClient client)
        {
            Client = client;
        }

        public void HandleTask()
        {
            model.MultiPlayerOption(new MultiPlayersInformation(Name, Client, NumberOfTask), size,NumberOfTask);
        }
    }
}

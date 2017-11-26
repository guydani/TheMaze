using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interface_Application;
using MazeLibary.SearchableClasses;
using Server.Interface;
using Server.Maze2D.Json;

namespace Server.Maze2D.Tasks
{
    public abstract class AbstractCreateMazeTask: ITask
    {
        public int NumberOfTask { get; set; }
        public string Name { get; set; }
        private IModel model;
        public List<int> Size;
        public AbstractCreateMaze<Cell, int, Position> AbstractCreateMaze { get; set; }

        public AbstractCreateMazeTask(IModel m)
        {
            model = m;
        }

        public abstract void SetCommand(string[] s, int index);

        public void HandleTask()
        {
            model.GenerateMazeSingle(new MazeProperty(Name), NumberOfTask, AbstractCreateMaze, Size);
        }

    }
}

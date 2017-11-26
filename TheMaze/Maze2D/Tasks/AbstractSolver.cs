using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;
using MazeLibary.Interface_Application;
using MazeLibary.SearchableClasses;
using Server.Interface;
using Server.Maze2D.Json;

namespace Server.Maze2D.Tasks
{
    public abstract class AbstractSolver: ITask
    {
        public int NumberOfTask { get; set; }
        public string Name { get; set; }
        private IModel model;
        public List<int> startPoint;
        public ISearcher<int> bfs { get; set; }

        public AbstractSolver(IModel m)
        {
            model = m;
        }

        public abstract void SetCommand(string[] s, int index);

        public void HandleTask()
        {
            var solve = new SolveProperty();;
            solve.Name = Name;
            model.GiveHint(solve , NumberOfTask, bfs, startPoint);
        }
    }

}


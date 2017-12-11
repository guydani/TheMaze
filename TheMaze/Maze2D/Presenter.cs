using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using Server.Interface;
using Server.Maze2D.Json;
using Server.Maze2D.Tasks;

namespace Server.Maze2D
{
    class Presenter : IPresenter
    {
        private IModel model;
        private IViewer viewer;
        private Dictionary<string, ITask> options;
        private TaskFactory taskFactory;
        private Mutex mut = new Mutex();

        public Presenter(IModel m, IViewer v)
        {
            model = m;
            viewer = v;
            viewer.MessageRecivedWaitToExecute += AddTaskToThreadPool;
            model.DoneWork += EndWork;
            CreateOptionsDictionary();
            taskFactory = new TaskFactory();
        }

        private void CreateOptionsDictionary()
        {
            options = new Dictionary<string, ITask>();
            options.Add("generate1", new CreateMazeDFS(model));
            options.Add("generate2", new CreateMazeKruskal(model));
            options.Add("generate3", new CreateMazePrim(model));
            options.Add("solve1", new BfsSolver(model));
            options.Add("solve2", new BestFsSolver(model));
            options.Add("games_waiting", new GamesWaitingTasks(model));
            options.Add("multiplayer", new MultiPlayerTask(model));
            options.Add("move", new MoveTask(model));
            options.Add("close", new CloseTask(model));
        }

        private int GetIndexTask()
        {
            Random random = new Random();
            int index = random.Next();
            while (viewer.ClientSaver.ContainsKey(index))
            {
                index = random.Next();
            }
            return index;
        }

        /* need to lock before getting a number until ending we input the task to place */
        public void AddTaskToThreadPool(string s, IClient c)
        {
            mut.WaitOne();
            int index = GetIndexTask();
            string[] commands = s.Split(' ');
            ITask task = options[commands[0]];
            try
            {
                task.SetCommand(commands, index);
                if (s.Contains("multiplayer"))
                {
                    ((MultiPlayerTask)task).SetMoreDetailes(c);
                }
            }
            catch (Exception exception)
            {
                viewer.ClientSaver[index] = c;
                viewer.SendMessage(exception.Message, index);
                return;
            }
            if (s.Contains("move") || s.Contains("close"))
            {

                var multiPlayerInformation = model.MultiPlayerInformation[commands[1]];
                if (multiPlayerInformation.FirstClient.Equals(c))
                {
                    viewer.ClientSaver[index] = multiPlayerInformation.SecondClient;
                }
                else
                {
                    viewer.ClientSaver[index] = multiPlayerInformation.FirstClient;
                }
            }
            else
            {
                viewer.ClientSaver[index] = c;
            }
            mut.ReleaseMutex();
            if (s.Contains("move"))
            {
                model.MultiPlayerMoves[c].Enqueue((MoveTask)task);
            }
            else
            {
                taskFactory.StartNew(task.HandleTask);
            }
        }

        public void EndWork(string json, int index)
        {
            viewer.SendMessage(json, index);   
        }

    }
}
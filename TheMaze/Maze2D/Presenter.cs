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
        private int indexToFollow;

        public Presenter(IModel m, IViewer v)
        {
            model = m;
            viewer = v;
            viewer.MessageRecivedWaitToExecute += AddTaskToThreadPool;
            model.DoneWork += EndWork;
            CreateOptionsDictionary();
            taskFactory = new TaskFactory();
            indexToFollow = 1;
        }

        private void CreateOptionsDictionary()
        {
            options = new Dictionary<string, ITask>();
            options["generate1"] = new CreateMazeDFS(model);
            options["generate2"] = new CreateMazeKruskal(model);
            options["generate3"] = new CreateMazePrim(model);
            options["solve1"] = new BfsSolver(model);
            options["solve2"] = new BestFsSolver(model);
            options["games_waiting"] = new GamesWaitingTasks(model);
            options["multiplayer"] = new MultiPlayerTask(model);
            options["move"] = new MoveTask(model);
            options["close"] = new CloseTask(model);
        }

        private int GetIndexTask()
        {
            mut.WaitOne();
            int index = indexToFollow;
            indexToFollow++;
            mut.ReleaseMutex();
            return index;
        }

        /* need to lock before getting a number until ending we input the task to place */
        public void AddTaskToThreadPool(string s, IClient c)
        {
            /*important for the movetask */
            CreateOptionsDictionary();
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
                viewer.AddClientSaver(index, c);
                viewer.SendMessage(exception.Message, index);
                return;
            }
            if (s.Contains("move") || s.Contains("close"))
            {

                var multiPlayerInformation = model.MultiPlayerInformation[commands[1]];
                if (multiPlayerInformation.FirstClient.Equals(c))
                {
                    viewer.AddClientSaver(index, multiPlayerInformation.SecondClient);
                }
                else
                {
                    viewer.AddClientSaver(index, multiPlayerInformation.FirstClient);
                }
            }
            else
            {
                viewer.AddClientSaver(index, c);
            }
            if (s.Contains("move"))
            {
                Console.WriteLine("index: " + task.NumberOfTask.ToString() + " direction = " + ((MoveTask)task).Direction);
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
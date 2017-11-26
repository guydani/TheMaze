using MazeLibary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.SearcherClasses;

namespace MazeLibary.SearchableClasses
{
    public abstract class AbstractCreateMaze<ICell, T, IPosition> : ICreator<ICell, T, IPosition> where T : IComparable
    {
        private IMaze<ICell<T>, T, IPosition<T>> maze;

        protected void GetSingleStartEndPoint()
        {
            foreach (ICell<T> i in maze)
            {
                i.IsVisited = false;
                i.Cost = Int32.MaxValue;
            }
            List<ICell<T>> l = new List<ICell<T>>();
            Random random = new Random();
            foreach (ICell<T> i in maze)
            {
                if (i.Neighbors.Count == 1)
                {
                    l.Add(i);
                }
            }
            maze.Entrance = l[random.Next(0, l.Count)];

            var startPoint = Maze.Entrance;
            Stack<ICell<T>> stack = new Stack<ICell<T>>();
            startPoint.Cost = 0;
            stack.Push(startPoint);
            while (stack.Count > 0)
            {
                var temp = stack.Pop();
                if (!temp.IsVisited)
                {
                    temp.IsVisited = true;
                    foreach (var cell in temp.Neighbors)
                    {
                        if (!cell.IsVisited)
                        {
                            cell.Cost = temp.Cost + 1;
                        }
                    }
                    temp.Neighbors.ForEach(x => stack.Push(x));
                }
            }
            int max = 0;
            ICell<T> tempCell = null;
            foreach (ICell<T> i in Maze)
            {
                if (i.Cost > max)
                {
                    max = i.Cost;
                    tempCell = i;
                }
            }
            Maze.Exit = tempCell;
        }

        /* need to use "GetSingleStartEndPoint()" before this function*/
        public void GetMultiStartEndPoint()
        {
            foreach (ICell<T> i in maze)
            {
                i.IsVisited = false;
                i.Cost = Int32.MaxValue;
            }
            List<ICell<T>> l = new List<ICell<T>>();
            Random random = new Random();
            foreach (ICell<T> i in maze)
            {
                if (i.Neighbors.Count == 1)
                {
                    l.Add(i);
                }
            }
            l.Remove(Maze.Entrance);
            ICell<T> tempCell = null;
            int temp;
            int distance = 0;
            foreach (var i in l)
            {
                maze.OptionalEntrance = i;
                GetMultiExit();
                temp = Maze.OptionalExit.Cost;
                if (temp > distance)
                {
                    distance = temp;
                    tempCell = Maze.OptionalEntrance;
                }
            }
            maze.OptionalEntrance = tempCell;
            GetMultiExit();
        }

        private void GetMultiExit()
        {
            foreach (ICell<T> cell in maze)
            {
                cell.IsVisited = false;
            }
            var startPoint = Maze.Entrance;
            Stack<ICell<T>> stack = new Stack<ICell<T>>();
            startPoint.Cost = 0;
            stack.Push(startPoint);
            while (stack.Count > 0)
            {
                var temp = stack.Pop();
                if (!temp.IsVisited)
                {
                    temp.IsVisited = true;
                    foreach (var cell in temp.Neighbors)
                    {
                        if (!cell.IsVisited)
                        {
                            cell.Cost = temp.Cost + 1;
                        }
                    }
                    temp.Neighbors.ForEach(x => stack.Push(x));
                }
            }
            startPoint = Maze.OptionalEntrance;
            startPoint.Cost = 0;
            ICell<T> maxCell = null;
            int distance = Int32.MaxValue;
            foreach (ICell<T> cell in maze)
            {
                cell.IsVisited = false;
            }
            stack.Push(startPoint);
            while (stack.Count > 0)
            {
                var temp = stack.Pop();
                if (!temp.IsVisited)
                {
                    temp.IsVisited = true;
                    foreach (var i in temp.Neighbors)
                    {
                        if (!i.IsVisited && distance > Math.Abs(i.Cost - (temp.Cost + 1)))
                        {
                            distance = Math.Abs(i.Cost - (temp.Cost + 1));
                            maxCell = i;
                        }
                    }
                    temp.Neighbors.ForEach(x => x.Cost = temp.Cost + 1);
                    temp.Neighbors.ForEach(x => stack.Push(x));
                }
            }
            Maze.OptionalExit = maxCell;
        }


        protected bool CheckIfUnVisitedCell() 
        {
            foreach (ICell<T> i in Maze)
            {
                if (!i.IsVisited)
                    return true;
            }
            return false;
        }

        public IMaze<ICell<T>, T, IPosition<T>> Maze
        {
            get { return maze; }
            set
            {
                maze = value;
                maze.InitialMaze();
                ICell<T> start = maze.CreateRandomStartPoint();
                Maze.Entrance = start;
                CreateMaze();
                Maze.UpdateNeighborsOfAllCells();
                GetSingleStartEndPoint();
                /*for MultiPlayer Game*/
                GetMultiStartEndPoint();
            }
        }

        public void InitialMaze()
        {
            maze.InitialMaze();
        }

        public abstract void CreateMaze();
    }
}

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
    /* this class implement the ICreator interface.
     * it holds the maze, and implement the methods that the creator of the game need.
     * it have to be virtual, because it depends on which cell we implement*/
    public abstract class AbstractCreateMaze<ICell, T, IPosition> : ICreator<ICell, T, IPosition> where T : IComparable
    {
        private IMaze<ICell<T>, T, IPosition<T>> maze;

        /* to make the maze work for one or two player, we need to make some changes.
         * if we play one player, we use this method, and use the maze.Entrance and the maze.Exit */

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

        /* this method find the other start point, and the end point of the game.
         * for two player we use the maze.Entrance, maze.OptionalEntrance, and for the end point, only
         * the maze.optionalExit. we need to use "GetSingleStartEndPoint()" before this function */

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

        /* this is a private method, only the method above use it. it help to maximize, the exit of both players
         * and make the exit fair to both sides */

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

        /* check if there is cell unvisited. because Imaze iterable, it goes over only the cells and not the position */
        protected bool CheckIfUnVisitedCell() 
        {
            foreach (ICell<T> i in Maze)
            {
                if (!i.IsVisited)
                    return true;
            }
            return false;
        }

        /* the maze creates him self, for single and multi player.
         * and each user, use what he needs */
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

        /* initial the maze */
        public void InitialMaze()
        {
            maze.InitialMaze();
        }

        public abstract void CreateMaze();
    }
}

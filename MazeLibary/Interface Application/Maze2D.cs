using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;

namespace MazeLibary.Interface_Application
{
    public class Maze2D:IMaze<ICell<int>, int, IPosition<int>>
    {
        protected ICell<int> entrance;
        public ICell<int> OptionalEntrance { get; set; }
        protected ICell<int> exit;
        public ICell<int> OptionalExit { get; set; }
        private List<int> border;
        protected IPosition<int>[,] maze;

        public Maze2D(List<int> b)
        {
            border = b;
        }

        public ICell<int> Entrance
        {
            get
            {
                return entrance;
            }

            set
            {
                entrance = value;
            }
        }

        public ICell<int> Exit
        {
            get
            {
                return exit;
            }

            set
            {
                exit = value;
            }
        }

        public List<int> Border
        {
            get
            {
                return border;
            }

            set
            {
                border = value;
            }
        }

        public ICell<int> GetCellInPosition(ICell<int> cell)
        {
            var l = cell.GetPosition;
            return (Cell) maze[l[0], l[1]];
        }

        private IPosition<int> GetWallBetweenTwoCell(ICell<int> current, ICell<int> newCell)
        {
            var l1 = current.GetPosition;
            var l2 = newCell.GetPosition;
            IPosition<int> position;
            if (l1[0] == l2[0])
            {
                if (l1[1] > l2[1])
                {
                    position = maze[l1[0], l1[1] - 1];
                }
                else
                {
                    position = maze[l1[0], l2[1] - 1];
                }
            }
            else
            {
                if (l1[0] > l2[0])
                {
                    position = maze[l1[0] - 1, l1[1]];
                }
                else
                {
                    position = maze[l2[0] - 1, l1[1]];
                }
            }
            return position;
        }

        public void BreakWallBetweenTwoCells(ICell<int> current, ICell<int> newCell)
        {
            IPosition<int> position = GetWallBetweenTwoCell(current, newCell);
            maze[position.GetPosition[0], position.GetPosition[1]].IsWall = false;
        }

        public void InitialMaze()
        {
            maze = new IPosition<int>[border[0], border[1]];
            for (int i = 0; i < border[0]; i++)
            {
                for (int j = 0; j < border[1]; j++)
                {
                    var l = new List<int>();
                    l.Add(i);
                    l.Add(j);
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        maze[i, j] = new Cell(l, true, new List<ICell<int>>());
                    }
                    else
                    {
                        maze[i, j] = new Position(l, true);
                    }
                }
            }
        }

        public ICell<int> CreateRandomStartPoint()
        {
            Random random = new Random();
            int i, j;
            i = random.Next(0, border[0]);
            while (i % 2 != 0)
            {
                i = random.Next(0, border[0]);
            }
            j = random.Next(0, border[0]);
            while (j % 2 != 0)
            {
                j = random.Next(0, border[0]);
            }
            return (Cell)maze[i, j];
        }

        public List<IPosition<int>> GetListOfAllWalls()
        {
            var l = new List<IPosition<int>>();
            for (int i = 0; i < border[0]; i++)
            {
                for (int j = 0; j < border[1]; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 != 0)
                        {
                            l.Add(maze[i,j]);
                        }
                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            l.Add(maze[i, j]);
                        }
                    }
                }
            }
            return l;
        }

        public List<IPosition<int>> GetListOfWalls(ICell<int> cell)
        {
            List<IPosition<int>> l = new List<IPosition<int>>();
            var i = cell.GetPosition[0];
            var j = cell.GetPosition[1];
            if (i - 1 > -1 && maze[i - 1 ,j].IsWall)
            {
                l.Add(maze[i - 1, j]);
            }
            if (i + 1 < border[0] && maze[i + 1, j].IsWall)
            {
                l.Add(maze[i + 1, j]);
            }
            if (j - 1 > -1 && maze[i, j - 1].IsWall)
            {
                l.Add(maze[i, j - 1]);
            }
            if (j + 1 < border[1] && maze[i, j + 1].IsWall)
            {
                l.Add(maze[i, j + 1]);
            }
            return l;
        }

        public string GetMazePresentation()
        {
            var s = "";
            for (int i = -1; i < border[0] + 1; i++)
            {
                for (int j = -1; j < border[1] + 1; j++)
                {
                    if (i == -1 || i == border[0])
                    {
                        s += '_';
                        if (i == -1 && j == border[1])
                        {
                            s += '\n';
                        }
                    } else if (j == -1)
                    {
                        s += '|';
                    } else if (j == border[1])
                    {
                        s += '|';
                        s += '\n';
                    }
                    else if (i == entrance.GetPosition[0] &&
                             j == entrance.GetPosition[1])
                    {
                        s += '*';
                    } else if (i == exit.GetPosition[0] &&
                               j == exit.GetPosition[1])
                    {
                        s += '#';
                    }
                    else if (maze[i, j].IsWall)
                    {
                        s += '1';
                    }
                    else if(!maze[i, j].IsWall)
                    {
                        s += '0';
                    } 
                }
            }
            return s;
        }

        public string GetMazePresentationForMultiPlayer()
        {
            var s = "";
            for (int i = -1; i < Border[0] + 1; i++)
            {
                for (int j = -1; j < Border[1] + 1; j++)
                {
                    if (i == -1 || i == Border[0])
                    {
                        s += '_';
                        if (i == -1 && j == Border[1])
                        {
                            s += '\n';
                        }
                    }
                    else if (j == -1)
                    {
                        s += '|';
                    }
                    else if (j == Border[1])
                    {
                        s += '|';
                        s += '\n';
                    }
                    else if (i == entrance.GetPosition[0] &&
                             j == entrance.GetPosition[1] || i == OptionalEntrance.GetPosition[0]
                             && j == OptionalEntrance.GetPosition[1])
                    {
                        s += '*';
                    }
                    else if (i == OptionalExit.GetPosition[0] &&
                             j == OptionalExit.GetPosition[1])
                    {
                        s += '#';
                    }
                    else if (maze[i, j].IsWall)
                    {
                        s += '1';
                    }
                    else if (!maze[i, j].IsWall)
                    {
                        s += '0';
                    }
                }
            }
            return s;
        }

        public string GetSolutionMazePresentation(Solution<int> solution)
        {
            string s = GetMazePresentation();
            StringBuilder sb = new StringBuilder(s);
            for (int i = 0; i < solution.SolutionList.Count; i++)
            {
                ICell<int> firstCell = solution.SolutionList[i];
                if (i + 1 == solution.SolutionList.Count)
                {
                    break;
                }
                ICell<int> secondCell = solution.SolutionList[i + 1];
                sb[(firstCell.GetPosition[0] + 1) * (border[1] + 3) + (firstCell.GetPosition[1] + 1)] = '2';
                sb[(secondCell.GetPosition[0] + 1) * (border[1] + 3) + (secondCell.GetPosition[1] + 1)] = '2';
                var wall = GetWallBetweenTwoCell(firstCell, secondCell);
                sb[(wall.GetPosition[0] + 1) * (border[1] + 3) + (wall.GetPosition[1] + 1)] = '2';
            }
            sb[(Entrance.GetPosition[0] + 1) * (border[1] + 3) + (Entrance.GetPosition[1] + 1)] = '*';
            sb[(Exit.GetPosition[0] + 1) * (border[1] + 3) + (Exit.GetPosition[1] + 1)] = '#';
            s = sb.ToString();
            return s;
        }

        public List<ICell<int>> GetWhichCellsThisWallSeperate(IPosition<int> wall)
        {
            var i = wall.GetPosition[0];
            var j = wall.GetPosition[1];
            List < ICell < int >> l = new List<ICell<int>>();
            if (i % 2 == 0)
            {
                l.Add((Cell) maze[i, j - 1]);
                l.Add((Cell) maze[i, j + 1]);
            }
            else
            {
                l.Add((Cell)maze[i + 1, j]);
                l.Add((Cell)maze[i - 1, j]);
            }
            return l;
        }

        public virtual void SetCostForCellsInMaze()
        {
            foreach (var i in maze)
            {
                if (i is Cell)
                {
                    ((Cell)i).Cost = 1;
                }
            }
        }

        public void UpdateNeighborsOfAllCells()
        {
            for (int i = 0; i < border[0]; i++)
            {
                for (int j = 0; j < border[1]; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        var l = new List<ICell<int>>();
                        if (i - 1 > -1 && !maze[i - 1, j].IsWall)
                        {
                            l.Add((ICell<int>) maze[i - 2, j]);
                        }
                        if (i + 1 < border[0] && !maze[i + 1, j].IsWall)
                        {
                            l.Add((ICell<int>) maze[i + 2, j]);
                        }
                        if (j - 1 > -1 && !maze[i, j - 1].IsWall)
                        {
                            l.Add((ICell<int>) maze[i, j - 2]);
                        }
                        if (j + 1 < border[1] && !maze[i, j + 1].IsWall)
                        {
                            l.Add((ICell<int>) maze[i, j + 2]);
                        }
                        ((ICell<int>) maze[i, j]).Neighbors = l;
                    }
                }
            }
        }

        //go over every cell(not positions) in the maze
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < border[0]; i++)
            {
                for (int j = 0; j < border[1]; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        yield return maze[i, j];
                    }
                }
            } 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interface_Application;
using MazeLibary.Interfaces;
using MazeLibary.SearchableClasses;
using Server.Interface;
using Server.Maze2D;
using Server.Maze2D.Json;
using Server.Maze2D.Tasks;


namespace Server
{
    public class Model: IModel
    {
        public event DoneWorking DoneWork;
        public Dictionary<string, IMaze<ICell<int>, int, IPosition<int>>> Games { get; }
        public Dictionary<string, MultiPlayersInformation> MultiPlayerInformation { get; }

        public Model()
        {
            Games = new Dictionary<string, IMaze<ICell<int>, int, IPosition<int>>>();
            MultiPlayerInformation = new Dictionary<string, MultiPlayersInformation>();
        }

        public void GenerateMazeSingle(MazeProperty mazeProperty, int index,
            AbstractCreateMaze<Cell, int, Position> createMaze, List<int> s)
        {
            IMaze<ICell<int>, int, IPosition<int>> maze = new MazeLibary.Interface_Application.Maze2D(s);
            createMaze.Maze = maze;
            mazeProperty.StartPoint = createMaze.Maze.Entrance.GetPosition;
            mazeProperty.EndPoint = createMaze.Maze.Exit.GetPosition;
            mazeProperty.MazePresentation = createMaze.Maze.GetMazePresentation();
            Games[mazeProperty.Name] = createMaze.Maze;
            OnDoneWorking(mazeProperty.SerializeClass(), index);
        }

        public void GiveHint(SolveProperty solveProperty, int index,
            ISearcher<int> bfs, List<int> s)
        {
            IMaze<ICell<int>, int, IPosition<int>> maze = Games[solveProperty.Name];
            var tempExit = maze.Exit;
            var tempEntrance = maze.Entrance;
            maze.Entrance.GetPosition = s;
            /*its a multiplayer game */
            if (MultiPlayerInformation.ContainsKey(solveProperty.Name))
            {
                maze.Exit = maze.OptionalExit;
            }
            var searchMaze = new SearchableMaze<int>(maze);
            searchMaze.SetInitialState(maze.GetCellInPosition(new Cell(s, false, null)));
            var solution = bfs.search(searchMaze);
            solveProperty.HintPresentation = maze.GetSolutionMazePresentation(solution);
            maze.Exit = tempExit;
            maze.Entrance = tempEntrance;
            OnDoneWorking(solveProperty.SerializeClass(), index);
        }

        public void GetMultiPlayersWaiting(int index)
        {
            GamesToPlay games = new GamesToPlay();
            foreach (var item in MultiPlayerInformation)
            {
                if (!item.Value.isFullGame())
                {
                    games.AddAnotherGame(item.Key);
                }   
            }
            OnDoneWorking(games.SerializeClass(), index);
        }

        public void MultiPlayerOption(MultiPlayersInformation multiPlayer, List<int> size, int index)
        {
            if (!MultiPlayerInformation.ContainsKey(multiPlayer.Name))
            {
                MultiPlayerInformation[multiPlayer.Name] = multiPlayer;
            }
            else
            {
                var check = MultiPlayerInformation[multiPlayer.Name];
                if (check.isFullGame())
                {
                    OnDoneWorking("error, the game is full", index);
                }
                else
                {
                    check.SecondClient = multiPlayer.FirstClient;
                    check.Index2 = index;
                    IMaze<ICell<int>, int, IPosition<int>> maze = new MazeLibary.Interface_Application.Maze2D(size);
                    var kruskalAlgorithem= new KruskalAlgorithem<Cell, int, Position>();
                    kruskalAlgorithem.Maze = maze;
                    Games[check.Name] = kruskalAlgorithem.Maze;
                    var mazeProperty = new MazePropertyMultiPlayer();
                    mazeProperty.StartPoint = kruskalAlgorithem.Maze.Entrance.GetPosition;
                    mazeProperty.AdditionalStartPoint = kruskalAlgorithem.Maze.OptionalEntrance.GetPosition;
                    mazeProperty.EndPoint = kruskalAlgorithem.Maze.OptionalExit.GetPosition;
                    OnDoneWorking(mazeProperty.SerializeClass(), check.Index1);
                    mazeProperty.StartPoint = kruskalAlgorithem.Maze.OptionalEntrance.GetPosition;
                    mazeProperty.AdditionalStartPoint = kruskalAlgorithem.Maze.Entrance.GetPosition;
                    OnDoneWorking(mazeProperty.SerializeClass(), check.Index2);
                }
            }
        }

        public void OnDoneWorking(string s, int index)
        {
            if (DoneWork != null)
                DoneWork(s, index);
        }

    }
}

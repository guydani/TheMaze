using System.Collections.Generic;
using MazeLibary.Interface_Application;
using MazeLibary.Interfaces;
using MazeLibary.SearchableClasses;
using Server.Maze2D;
using Server.Maze2D.Json;
using Server.Maze2D.Tasks;

namespace Server.Interface
{
    public delegate void DoneWorking(string json, int index);
    public interface IModel
    {
        event DoneWorking DoneWork;
        Dictionary<string, IMaze<ICell<int>, int, IPosition<int>>> Games { get; }
        Dictionary<string, MultiPlayersInformation> MultiPlayerInformation { get; }
        Dictionary<IClient, PriorityQueue> MultiPlayerMoves { get; }
        void GenerateMazeSingle(MazeProperty mazeProperty, int index,
            AbstractCreateMaze<Cell, int, Position> createMaze, List<int> s);
        void GiveHint(SolveProperty solveProperty, int index,
            ISearcher<int> bfs, List<int> s);
        void GetMultiPlayersWaiting(int index);
        void MultiPlayerOption(MultiPlayersInformation multiPlayer, List<int> size, int index);
        void MultiPlayerMove(string direction, int numberOfTask);
        void CloseOption(string name, int numberOfTask);
        void OnDoneWorking(string s, int index);
    }
}

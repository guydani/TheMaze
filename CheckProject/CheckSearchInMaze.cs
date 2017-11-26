using System;
using System.Collections.Generic;
using MazeLibary.Interfaces;
using MazeLibary.Interface_Application;
using MazeLibary.SearchableClasses;
using MazeLibary.SearcherClasses;

namespace CheckProject
{
    class CheckSearchInMaze
    {
        static void Main(string[] args)
        {
            AbstractSearcher<int> searcher = new BFSAlgorithem<int>();
            List<int> l = new List<int>();
            l.Add(3 * 2 - 1); l.Add(3 * 2 - 1);
            IMaze<ICell<int>, int, IPosition<int>> maze = new Maze2DWithRandomCost(l);
            SearchableMaze<int> searchableMaze = new SearchableMaze<int>(maze);
            AbstractCreateMaze<Cell, int, Position> createMaze = new PrimsAlgorithem<Cell, int, Position>();
            createMaze = new PrimsAlgorithem<Cell, int, Position>();
            createMaze.Maze = maze;
            ISearcher<int> bfs = new BFSAlgorithem<int>();
            var bestfs = new BestFSAlforithem<int>();
            var solution = bfs.search(searchableMaze);
            Console.WriteLine(searchableMaze.Maze.GetSolutionMazePresentation(solution));
            var position = new List<int>();
            position.Add(0);
            position.Add(0);
            var end = new List<int>();
            end.Add(4);
            end.Add(4);
            searchableMaze.Maze.Entrance.GetPosition = position;
            searchableMaze.Maze.Exit.GetPosition = end;
            solution = bfs.search(searchableMaze);
            Console.WriteLine(searchableMaze.Maze.GetSolutionMazePresentation(solution));
            //Console.WriteLine(createMaze.Maze.GetSolutionMazePresentation(solution));
            //Console.WriteLine(solution.NumberOfNodesEvaluated);
            //solution = bestfs.search(searchableMaze);
            //Console.WriteLine(createMaze.Maze.GetSolutionMazePresentation(solution));
            //Console.WriteLine(solution.NumberOfNodesEvaluated);
        }
    }
}

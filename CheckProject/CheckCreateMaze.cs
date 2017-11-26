//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MazeLibary.Interfaces;
//using MazeLibary.Interface_Application;
//using MazeLibary.SearchableClasses;

//namespace CheckProject
//{
//    class CheckCreateMaze
//    {
//        static void Main(string[] args)
//        {
//            AbstractCreateMaze<Cell, int, Position> createMaze = new DFSCreateMaze<Cell, int, Position>();
//            var l = new List<int>();
//            l.Add(5 * 2 - 1);
//            l.Add(5 * 2 - 1);
//            //            IMaze<ICell<int>, int, IPosition<int>> maze = new Maze2D(l);
//            Maze2D maze = new Maze2D(l);
//            createMaze.Maze = maze;
//            Console.WriteLine(maze.GetMazePresentation());
//            Console.WriteLine(maze.GetMazePresentationForMultiPlayer());
//            Console.WriteLine(createMaze.Maze.GetMazePresentation());
//            Console.WriteLine("\n\n\n");
//            createMaze = new KruskalAlgorithem<Cell, int, Position>();
//            createMaze.Maze = maze;
//            Console.WriteLine(createMaze.Maze.GetMazePresentation());
//            Console.WriteLine(maze.GetMazePresentationForMultiPlayer());
//            Console.WriteLine("\n\n\n");
//            createMaze = new PrimsAlgorithem<Cell, int, Position>();
//            createMaze.Maze = maze;
//            Console.WriteLine(createMaze.Maze.GetMazePresentation());
//            Console.WriteLine(maze.GetMazePresentationForMultiPlayer());
//            Console.WriteLine("\n\n\n");
//        }  
//    }
//}

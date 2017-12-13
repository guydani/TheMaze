using MazeLibary.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.Interfaces
{
    /*
        the maze, responsible for all the aspects off the maze, to move on him,
        the entrance and etc. we have two entrance for multiplayer game. 
        if we play multiplayer, we use both entrance and the optional exit.
        else we use the exit.
        some of the methods for creating the maze.
    */
    public interface IMaze<ICell, T, IPosition>: IEnumerable where T : IComparable
    {
        ICell<T> Entrance { get; set; }
        ICell<T> OptionalEntrance { get; set; }
        ICell<T> Exit { get; set; }
        ICell<T> OptionalExit { get; set; }
        List<T> Border { get; set; }
        ICell<T> GetCellInPosition(ICell<T> cell);
        void BreakWallBetweenTwoCells(ICell<T> current, ICell<T> newCell);
        void InitialMaze();
        ICell<T> CreateRandomStartPoint();
        List<IPosition<T>> GetListOfAllWalls();
        List<IPosition<T>> GetListOfWalls(ICell<T> cell);
        string GetMazePresentation();
        string GetMazePresentationForMultiPlayer();
        string GetSolutionMazePresentation(Solution<T> solution);
        List<ICell<T>> GetWhichCellsThisWallSeperate(IPosition<T> wall);
        void SetCostForCellsInMaze();
        void UpdateNeighborsOfAllCells();
    }
}

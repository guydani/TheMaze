using MazeLibary.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.Interfaces
{
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
        String GetMazePresentation();
        string GetMazePresentationForMultiPlayer();
        String GetSolutionMazePresentation(Solution<T> solution);
        List<ICell<T>> GetWhichCellsThisWallSeperate(IPosition<T> wall);
        void SetCostForCellsInMaze();
        void UpdateNeighborsOfAllCells();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.Interfaces
{
    /* 
        here we define the things that we have to in a maze.
        for flexibility, the pattern we use is bridge.
        */
    public interface ISearchable<T> where T : IComparable
    {
        ICell<T> GetInitialState();
        ICell<T> GetGoalState();
        List<ICell<T>> GetAllPossibleStates(ICell<T> cell);
        void MakeAllCellUnVisited();

    }
}

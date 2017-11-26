using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.Interfaces
{
    public interface ISearchable<T> where T : IComparable
    {
        ICell<T> GetInitialState();
        ICell<T> GetGoalState();
        List<ICell<T>> GetAllPossibleStates(ICell<T> cell);
        void MakeAllCellUnVisited();

    }
}

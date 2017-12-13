using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibary.Interfaces
{
    /* 
        this interface define the things we need for solve the maze.
        the second method, is for analyzing the search class.
        */
    public interface ISearcher<T> where T: IComparable
    {
        // the search method
        Solution<T> search(ISearchable<T> searchable);
        // get how many nodes were evaluated by the algorithm
        int getNumberOfNodesEvaluated();

    }
}

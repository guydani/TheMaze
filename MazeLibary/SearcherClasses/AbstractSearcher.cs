using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;

namespace MazeLibary.SearcherClasses
{
    public abstract class AbstractSearcher<T>: ISearcher<T> where T: IComparable
    {
        private int numberOfNodesEvaluated = 0;
        protected MyQueue<ICell<T>, T> openList;

        private void addAnotherNodeEvaluated()
        {
            numberOfNodesEvaluated += 1;
        }

        public Solution<T> search(ISearchable<T> searchable)
        {
            searchable.MakeAllCellUnVisited();
            ICell<T> cell = null;
            openList.Enqueue(searchable.GetInitialState());
            searchable.GetInitialState().IsVisited = true;
            searchable.GetInitialState().CameFrom = null;
            while (!openList.isEmpty())
            {
                cell = openList.Dequeue();
                addAnotherNodeEvaluated();
                if (cell.Equals(searchable.GetGoalState()))
                {
                    return backtrace(searchable);
                }
                foreach (var i in cell.Neighbors)
                {
                    addAnotherNodeEvaluated();
                    if (!i.IsVisited)
                    {
                        i.CameFrom = cell;
                        i.IsVisited = true;
                        i.Cost = cell.Cost + i.Cost;
                        openList.Enqueue(i);
                    }
                }
            }
            return null;
        }

        private Solution<T> backtrace(ISearchable<T> searchable)
        {
            ICell<T> cell = searchable.GetGoalState();
            Solution<T> solution = new Solution<T>();
            solution.add(cell);
            while (cell != null)
            {
                cell = cell.CameFrom;
                solution.add(cell);
            }
            solution.SolutionList.Remove(null);
            solution.NumberOfNodesEvaluated = getNumberOfNodesEvaluated();
            return solution;
        }

        public int getNumberOfNodesEvaluated()
        {
            return numberOfNodesEvaluated;
        }
    }
}

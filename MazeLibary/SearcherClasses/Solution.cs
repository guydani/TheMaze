using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interfaces;

namespace MazeLibary
{
    public class Solution<T> where T: IComparable
    {
        public List<ICell<T>> SolutionList { get; set; }
        public int NumberOfNodesEvaluated { get; set; }

        public Solution()
        {
            this.SolutionList = new List<ICell<T>>();
        }

        public void add(ICell<T> pos)
        {
            SolutionList.Add(pos);
        }

    }
}

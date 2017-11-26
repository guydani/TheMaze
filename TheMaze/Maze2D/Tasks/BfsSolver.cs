using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.SearcherClasses;
using Server.Interface;

namespace Server.Maze2D.Tasks
{
    public class BfsSolver: AbstractSolver
    {
        public override void SetCommand(string[] s, int index)
        {
            try
            {
                Name = s[1];
                startPoint = new List<int>();
                startPoint.Add(Int32.Parse(s[2]));
                startPoint.Add(Int32.Parse(s[3]));
                bfs = new BFSAlgorithem<int>();
                NumberOfTask = index;
            }
            catch (Exception)
            {
                throw new Exception("problem with command");
            }
        }

        public BfsSolver(IModel m) : base(m)
        {
        }
    }
}

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.SearcherClasses;
using Server.Interface;

namespace Server.Maze2D.Tasks
{
    class BestFsSolver: AbstractSolver
    {
        public override void SetCommand(string[] s, int index) 
        {
            try
            {
                Name = s[1];
                startPoint = new List<int>();
                startPoint.Add(Int32.Parse(s[2]));
                startPoint.Add(Int32.Parse(s[3]));
                bfs = new BestFSAlforithem<int>();
                NumberOfTask = index;
            }
            catch (Exception)
            {
                throw new Exception("problem with command");
            }

        }

        public BestFsSolver(IModel m) : base(m)
        {
        }
    }
}

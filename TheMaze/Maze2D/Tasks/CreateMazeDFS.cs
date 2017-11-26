using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLibary.Interface_Application;
using MazeLibary.SearchableClasses;
using MazeLibary.SearcherClasses;
using Server.Interface;
using Server.Maze2D.Json;

namespace Server.Maze2D.Tasks
{
    class CreateMazeDFS: AbstractCreateMazeTask
    {

        public override void SetCommand(string[] s, int index)
        {
            try
            {
                Name = s[1];
                Size = new List<int>();
                Size.Add(Int32.Parse(s[2]));
                Size.Add(Int32.Parse(s[3]));
                AbstractCreateMaze = new DFSCreateMaze<Cell, int, Position>();
                NumberOfTask = index;
            }
            catch (Exception)
            {
                throw new Exception("problem with command");
            }
        }

        public CreateMazeDFS(IModel m) : base(m)
        {
        }
    }
}

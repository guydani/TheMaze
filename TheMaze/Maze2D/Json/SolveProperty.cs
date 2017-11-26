using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Maze2D.Json
{
    public class SolveProperty: AbstractJson
    {
        public SolveProperty() { }
        public string Name { get; set; }
        public string HintPresentation { get; set; }
    }
}

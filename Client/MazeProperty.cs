using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MazeProperty
    {
        public string Name { get; set; }
        public List<int> StartPoint { get; set; }
        public List<int> EndPoint { get; set; }
        public string MazePresentation { get; set; }
    }
}

using System.Collections.Generic;
using Server.Interface;

namespace Server.Maze2D.Json
{
    public class MazeProperty: AbstractJson
    {
        /* the message sending to the clien */
        public string Name { get; set; }
        public List<int> StartPoint { get; set; }
        public List<int> EndPoint { get; set; }
        public string MazePresentation { get; set; }

        public MazeProperty()
        {
        }

        public MazeProperty(string s)
        {
            Name = s;
        }
    }
}
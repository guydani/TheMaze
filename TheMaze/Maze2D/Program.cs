using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Interface;
using Server.Maze2D;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IModel model = new Model();
            IViewer viewer = new Viewer();
            IPresenter presenter = new Presenter(model, viewer);
            viewer.Start();
        }
    }
}

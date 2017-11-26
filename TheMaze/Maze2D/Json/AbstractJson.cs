using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Server.Interface;

namespace Server.Maze2D.Json
{
    public abstract class AbstractJson: IJson
    {
        public string SerializeClass()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

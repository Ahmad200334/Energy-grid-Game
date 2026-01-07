using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enery_gridGame
{
    public abstract class BaseReport
    {

        public string Path { get; set; }
        public int Cost { get; set; }
        public int Count { get; set; }

        public int allState { set; get; } = 0;


        public abstract void start();
    }
}

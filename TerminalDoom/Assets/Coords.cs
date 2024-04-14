using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom.Assets
{
    internal class Coords
    {
        public double x;
        public double y;

        public Coords()
        {
            this.x = 0.0;
            this.y = 0.0;
        }
        public Coords(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalDoom.Assets;

namespace TerminalDoom
{
    internal class Player
    {
        public int FOV { get; set; }
        public Coords Pos { get; set; }
        public float Angle { get; set; }

        public Player(int fov, Coords position, float angle)
        {
            this.FOV = fov;
            this.Pos = position;
            this.Angle = angle;
        }
    }
}

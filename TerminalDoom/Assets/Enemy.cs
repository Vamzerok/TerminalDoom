using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom.Assets
{
    internal class Enemy : GameObject
    {
        public Enemy(Coords position, float angle, float speed)
        {
            this.Pos = position;
            this.Angle = angle;
            this.Speed = speed;
        }
    }
}

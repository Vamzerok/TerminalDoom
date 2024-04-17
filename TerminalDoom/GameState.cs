using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalDoom.Assets;

namespace TerminalDoom
{
    internal class GameState : GameLogic
    {
        public Player player;
        public List<GameObject> gameObjects;

        public double deltaTime
        {
            get
            {
                return (double) 1 / this.fps;
            }
        }
        public int fps;

        public Map map;

        public bool exit;


    }
}

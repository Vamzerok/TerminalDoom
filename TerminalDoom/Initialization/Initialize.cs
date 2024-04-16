using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalDoom.Assets;

namespace TerminalDoom.Initialization
{
    internal class Initialize
    {
        public static GameState Start()
        { 
            GameState state = new GameState();

            state.player = new Player(90,new Coords(0,0), 0);
            state.map = new Map("./map1.txt");
            state.exit = false;

            return state;
        }
    }
}

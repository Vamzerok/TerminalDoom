﻿using System;
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
           // Menu.Start();
            GameState state = new GameState();

            state.player = new TerminalDoom.Player(60,new Coords(1,1), 0);
            state.map = new Map("./map1.txt");
            state.exit = false;

            return state;
        }
    }
}

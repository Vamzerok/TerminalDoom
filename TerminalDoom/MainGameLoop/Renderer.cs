﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom
{
    internal class Renderer
    {
        public static void Render(GameState gameState)
        {
            Console.WriteLine(gameState.player.FOV);
        }
    }
}

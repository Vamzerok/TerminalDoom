using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom
{
    internal class GameLogic
    {
        //TODO: Player mozogjon user inputnak megfelelően
        public static GameState Update(GameState gameState, string input)
        {
            //angel ++ 360-nal 0
            //space keypress-> coords  

            double rotationSpeed = 5;

            gameState.player.Angle += rotationSpeed * gameState.deltaTime;
            if(gameState.player.Angle >= 360)
            {
                gameState.player.Angle = 0;
            }

            return gameState;
        }
    }
}

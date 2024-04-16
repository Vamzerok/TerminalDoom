using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom
{
    internal class GameLogic : Player, GameState
    {
        //TODO: Player mozogjon user inputnak megfelelően
        public static GameState Update(GameState gameState, string input)
        {
            //angel ++ 360-nal 0
            //space keypress-> coords++

            double speed = 1;
            int maximalize = 0;
            //Player player = new Player();
            

            if (maximalize != 360)
            {
                
                //player.Angle += speed;
                gameState.player.Angle += speed;
            }else if(maximalize == 360 ||  maximalize > 360)
            {
                maximalize = 0;
                player.Angle += speed;
                gameState.player.Angle += speed;

            }

            if(Encoding.ASCII.GetBytes(input) == 32)
            {
                gameState.player.Pos.x += gameState.deltaTime * Math.Cos(AngleToRadians(gameState.player.Angle));
                gameState.player.Pos.y += gameState.deltaTime * Math.Cos(AngleToRadians(gameState.player.Angle))
            }


            

            

           
            


            return gameState;
        }
    }
}

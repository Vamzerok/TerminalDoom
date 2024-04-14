using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TerminalDoom.Initialization;

namespace TerminalDoom
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //------------- [Initialization - Start] ------------- 

            GameState gameState = Initialize.Start();

            //------------- [Initialization - End] ------------- 

            //-------------[Main gameloop - Start] ------------- 
            while (true)
            {
                //game logic
                gameState = GameLogic.Update(gameState);

                //renderer
                Renderer.Render(gameState);
                Console.ReadKey();
            }
            //------------- [Main gameloop - End] ------------- 
        }
    }
}

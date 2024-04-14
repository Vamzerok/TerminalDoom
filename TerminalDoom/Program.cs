using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            byte[] framebuff = new byte[((Console.BufferHeight - 1) * Console.BufferWidth)];

            string Path = $"./{DateTime.Now.Month}{DateTime.Now.Day}.txt";
            string comment = "New codebase test";
            File.AppendAllText(Path, $"\n------------------------\t[{comment}]\n");

            Stream STDOUT = Console.OpenStandardOutput();

            Stopwatch stopw = new Stopwatch();
            long prevFrame = 0;
            int count = 0;
            int countUntilQuit = 5;
            stopw.Start();
            
            //-------------[Main gameloop - Start] ------------- 
            while (true)
            {
                long frameCalculationStart = stopw.ElapsedMilliseconds; //start measurement 
                //------------//------------//------------//------------

                //game logic
                gameState = GameLogic.Update(gameState);

                //renderer
                framebuff = Renderer.Render(gameState, framebuff);
                Renderer.DrawScreen(framebuff, STDOUT);

                //------------//------------//------------//------------
                count++;
                long frameCalculationEnd = stopw.ElapsedMilliseconds; //end measurement 

                if (stopw.ElapsedMilliseconds - prevFrame > 1000)
                {
                    File.AppendAllText(Path, $"{count}fps; {frameCalculationEnd - frameCalculationStart}ms; ({Console.BufferHeight},{Console.BufferWidth}); {stopw.ElapsedMilliseconds - prevFrame}ms\n");
                    Console.Beep(2000, 50);

                    if (countUntilQuit <= 1) return;
                    countUntilQuit--;

                    count = 0;
                    prevFrame = stopw.ElapsedMilliseconds;
                }
            }
            //------------- [Main gameloop - End] ------------- 
        }
    }
}

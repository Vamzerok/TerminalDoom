using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
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
            int[] inputbuff = new int[4];

            //only for debugging
            string Path = $"./{DateTime.Now.Month}{DateTime.Now.Day}.txt";
            string comment = "new Coords in raycaster while not found wall";
            File.AppendAllText(Path, $"\n------------------------\t[{comment}]\n");

            Stream STDOUT = Console.OpenStandardOutput();
            Stream STDIN = Console.OpenStandardInput();

            StreamReader sr = new StreamReader(STDIN);
            char[] input = new char[6];
            sr.ReadAsync(input, 0, 6);

            Stopwatch stopw = new Stopwatch();
            long prevFrame = 0;
            int count = 0;
            int countUntilQuit = 15;
            stopw.Start();

            Renderer rend = new Renderer(gameState);
            GameLogic gaml = new GameLogic();

            gameState.fps = 60;
            int frameCap = 60;
            
            //-------------[Main gameloop - Start] ------------- 
            while (true)
            {
                long frameCalculationStart = stopw.ElapsedMilliseconds;
                //------------//------------//------------//------------//start measurement 


                //game logic
                gameState = GameLogic.Update(gameState,"");

                //renderer
                rend.Render(gameState);
                
                //Renderer.DrawScreen(framebuff, STDOUT);

                //------------//------------//------------//------------//end measurement 
                count++;
                long frameCalculationEnd = stopw.ElapsedMilliseconds;

                if (stopw.ElapsedMilliseconds - prevFrame > 1000)
                {
                    File.AppendAllText(Path, $"{count}fps; {frameCalculationEnd - frameCalculationStart}ms; ({Console.BufferHeight},{Console.BufferWidth}); {stopw.ElapsedMilliseconds - prevFrame}ms\n");
                    gameState.fps = count;
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

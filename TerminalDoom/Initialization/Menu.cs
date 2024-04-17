using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom.Initialization
{
    internal class Menu
    {
        public static void Start()
        { 
                var STDOUT = Console.OpenStandardOutput();
                byte[] clearBuff = new byte[(Console.BufferHeight - 1) * Console.BufferWidth];


                Gomb g = new Gomb(ConsoleColor.Black, ConsoleColor.White, new Assets.Coords((Console.WindowWidth / 2) - 2, 10), "DOOM");

                Gomb g1 = new Gomb(ConsoleColor.White, ConsoleColor.Black, new Assets.Coords((Console.WindowWidth / 2) - 4.5, 13), "Select Map");

                Gomb g2 = new Gomb(ConsoleColor.White, ConsoleColor.Black, new Assets.Coords((Console.WindowWidth / 2) - 2, 16), "Start");

                g1.NextGomb = g2;
                g2.NextGomb = g1;

                g.Draw();
                g1.Draw();
                g2.Draw();
                Gomb activeGomb = g1;

                
            while (true)
            {
                Console.CursorVisible = false;
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.KeyChar == '.')
                {

                    activeGomb = activeGomb.NextGomb;
                    activeGomb.Backgroundcolor = ConsoleColor.Red;
                    activeGomb.ForGroundColor = ConsoleColor.Black;
                    g.Draw();
                    g1.Draw();
                    g2.Draw();
                    activeGomb.Backgroundcolor = ConsoleColor.White;
                    activeGomb.ForGroundColor = ConsoleColor.Black;


                }
                /* Console.Clear();
                 Gomb map1 = new Gomb(ConsoleColor.White, ConsoleColor.Black, new Assets.Coords(10, 10), "map1");

                 Gomb map2 = new Gomb(ConsoleColor.White, ConsoleColor.Black, new Assets.Coords(10, 13), "map2");

                 Gomb map3 = new Gomb(ConsoleColor.White, ConsoleColor.Black, new Assets.Coords(10, 16), "map3");
                 Gomb map4 = new Gomb(ConsoleColor.White, ConsoleColor.Black, new Assets.Coords(10, 19), "map4");
                 map1.Draw();
                 map2.Draw();
                 map3.Draw();
                 map4.Draw();

                 ConsoleKeyInfo input = Console.ReadKey();
                 if (input.KeyChar == 'w')
                 {
                     activeGomb.Backgroundcolor = ConsoleColor.Black;
                     activeGomb.ForGroundColor = ConsoleColor.White;
                     activeGomb = activeGomb.NextGomb;

                 }
             }
             STDOUT.Write(clearBuff, 0, clearBuff.Length);
                 */
            }
                    Console.ReadKey();
               }
         }
}


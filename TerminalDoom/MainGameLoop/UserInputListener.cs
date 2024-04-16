using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom
{
    class UserInputListener
    {
        public ConsoleKey input;

        public void GetUserInput()
        {
            if(Console.KeyAvailable)
            {
                input = Console.ReadKey().Key;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom.Initialization
{
    class Gomb
    {
        public ConsoleColor Backgroundcolor;
        public ConsoleColor ForGroundColor;
        public Assets.Coords position;
        public string Text;

        public Gomb NextGomb;
        public Gomb PrevGomb;

        public bool isActive;
        public Gomb(ConsoleColor bg, ConsoleColor fg, Assets.Coords position, string text)
        {
            Backgroundcolor = bg;
            ForGroundColor = fg;
            this.position = position;
            Text = text;
        }
        public void Draw()
        {
            Console.ForegroundColor = ForGroundColor;
            Console.BackgroundColor = Backgroundcolor;
            Console.CursorTop = (int)position.y;
            Console.CursorLeft = (int)position.x;
            Console.Write(Text);

        }
        public void Click()
        {

        }
    }
}

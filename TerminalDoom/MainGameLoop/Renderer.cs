using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom
{
    internal class Renderer
    {
        public static byte[] Render(GameState gameState, byte[] framebuff)
        {
            for (int i = 0; i < framebuff.Length; i++)
            {
                framebuff[i] = i % 2 + (i / 120 % 2) == 0 ? (byte)'@' : (byte)' ';
            }
            return framebuff;
        }

        public static void DrawScreen(byte[] framebuff, Stream STDOUT)
        {
            STDOUT.Write(framebuff, 0, framebuff.Length);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalDoom.Assets;

namespace TerminalDoom
{
    internal class Renderer
    {
        private GameState GameState;

        private double ScreenHeight;
        private double ScreenWidth;
        private double ScreenHalfWidth;
        private double ScreeenHalfHeight;
        private double PlayerHalfFov;
        private double RayCasterIncrementAngle;

        private double RayCastingPrecision;

        internal double DegToRad(double degrees)
        {
            return Math.PI * degrees / 180 ;
        }

        internal void RacCaster()
        {
            double rayAngle = GameState.player.Angle - PlayerHalfFov;

            for(int rayCount = 0; rayCount < ScreenWidth; rayCount++)
            {
                Coords ray = new Coords(GameState.player.Pos.x, GameState.player.Pos.y);

                double rayCos = Math.Cos(DegToRad(rayAngle)) / RayCastingPrecision;
                double raySin = Math.Sin(DegToRad(rayAngle)) / RayCastingPrecision;

                //findig wall
                bool foundWall = false;
                
                while (!foundWall)
                {
                    ray.x += rayCos;
                    ray.y += raySin;

                    if(GameState.map.Layout[(int)Math.Floor(ray.y), (int)Math.Floor(ray.x)] == 1) {
                        
                    }
                }
            }
        }

        public byte[] Render(GameState gameState, byte[] framebuff)
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

        public Renderer(GameState state)
        {
            GameState = state;

            ScreenWidth = Console.BufferWidth;
            ScreenHeight = Console.BufferHeight;
            ScreenHalfWidth = Console.BufferWidth / 2;
            ScreeenHalfHeight = Console.BufferHeight / 2;
            PlayerHalfFov = state.player.FOV / 2;
            RayCasterIncrementAngle = state.player.FOV / ScreenWidth;

            RayCastingPrecision = 64;
        }
    }
}

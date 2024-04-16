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
        private char[] Gradient;

        private double RayCastingPrecision;

        private byte[] framebuff;

        private Stream STDOUT;

        internal double DegToRad(double degrees)
        {
            return Math.PI * degrees / 180 ;
        }

        internal int ConvertToFramebuffCoords(int x, int y)
        {
            return framebuff.Length * y + x;
        }

        internal void DrawLineToFrameBuff(int x1, int y1, int x2, int y2, char gradient)
        {
            int point1 = ConvertToFramebuffCoords(x1, y1);
            int point2 = ConvertToFramebuffCoords(x2, y2);
            //framebuff[p]
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
                //Coords wall = new Coords();
                while (!foundWall)
                {
                    ray.x += rayCos;
                    ray.y += raySin;

                    Coords currentSearch = new Coords(Math.Floor(ray.x), Math.Floor(ray.y));

                    if(GameState.map.Layout[(int) currentSearch.y, (int)currentSearch.x] == 1) {
                        foundWall = true;
                        //wall.x = currentSearch.x;
                        //wall.y = currentSearch.y;
                    }
                }

                double playerRayXDelta = GameState.player.Pos.x - ray.x;
                double playerRayYDelta = GameState.player.Pos.y - ray.y;
                // Pythagoras theorem
                double disctance = Math.Sqrt(Math.Pow(playerRayXDelta, 2) + Math.Pow(playerRayYDelta, 2));

                int wallHeight = (int)Math.Floor(ScreeenHalfHeight / disctance);

                for(int i = 0; i < wallHeight; i++)
                {
                    framebuff[ConvertToFramebuffCoords
                        (x: rayCount
                        ,y: (int)ScreeenHalfHeight + wallHeight / 2 + wallHeight-1
                        )] = (byte)'@';
                }
            }
            DrawScreen(framebuff);
        }

        public void Render(GameState gameState)
        {
            GameState = gameState;
            RacCaster();
        }

        private void DrawScreen(byte[] framebuff)
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

            STDOUT = Console.OpenStandardOutput();

            this.framebuff = new byte[((Console.BufferHeight - 1) * Console.BufferWidth)];
            for(int i = 0; i < framebuff.Length; i++)
            {
                framebuff[i] = (byte)' ';
            }
        }
    }
}

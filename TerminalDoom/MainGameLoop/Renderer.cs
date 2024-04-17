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

        private int ScreenHeight;
        private int ScreenWidth;
        private double ScreenHalfWidth;
        private double ScreeenHalfHeight;
        private double PlayerHalfFov;
        private double RayCasterIncrementAngle;
        private string Gradient;

        private double RayCastingPrecision;

        private byte[] framebuff;

        private Stream STDOUT;

        internal double DegToRad(double degrees)
        {
            return Math.PI * degrees / 180 ;
        }

        internal int ConvertToFramebuffCoords(int left, int top)
        {
            return ScreenWidth * top + left;
        }

        internal void DrawLineToFrameBuff(int x1, int y1, int x2, int y2, char gradient)
        {
            int point1 = ConvertToFramebuffCoords(x1, y1);
            int point2 = ConvertToFramebuffCoords(x2, y2);
            //framebuff[p]
        }

        internal double CastRay(double rayAngle)
        {
           /* if (rayAngle == -18)
            {

            }*/
            Coords ray = new Coords(GameState.player.Pos.x, GameState.player.Pos.y);

            double rayCos = Math.Cos(DegToRad(rayAngle)) / RayCastingPrecision;
            double raySin = Math.Sin(DegToRad(rayAngle)) / RayCastingPrecision;

            //findig wall
            bool foundWall = false;
            int currentSearchX = 0;
            int currentSearchY = 0;
            while (!foundWall)
            {
                ray.x += rayCos;
                ray.y += raySin;

                currentSearchX = (int)Math.Floor(ray.x);
                currentSearchY = (int)Math.Floor(ray.y);

                if (GameState.map.Layout[currentSearchY, currentSearchX] == 1)
                {
                    foundWall = true;
                }

            }


            double playerRayXDelta = GameState.player.Pos.x - ray.x;
            double playerRayYDelta = GameState.player.Pos.y - ray.y;
            // Pythagoras theorem

            double distance = Math.Sqrt(Math.Pow(playerRayXDelta, 2) + Math.Pow(playerRayYDelta, 2));
            distance = distance * Math.Cos(DegToRad(rayAngle - GameState.player.Angle));

            return distance; 
        }

        internal void RayCaster()
        {
            double rayAngle = GameState.player.Angle - PlayerHalfFov;

            for(int rayCount = 0; rayCount < ScreenWidth; rayCount++)
            {
                double distance = (double) CastRay(rayAngle) / 3;
                int wallHeight = (int)Math.Floor(ScreeenHalfHeight / distance);

                byte grad = (byte)Gradient[(int)Math.Min(Gradient.Length - distance * ((double)Gradient.Length / 15), Gradient.Length - 1)];
                framebuff[ConvertToFramebuffCoords(rayCount, (int)ScreeenHalfHeight)] = grad;

                rayAngle += RayCasterIncrementAngle;

                //up 
                for(int i = 0; i < wallHeight; i++)
                {
                    try
                    {
                        framebuff[ConvertToFramebuffCoords
                        (left: rayCount
                        , top: (int)ScreeenHalfHeight + ((int)Math.Min(wallHeight, ScreeenHalfHeight - 1)) - ((int)Math.Min(wallHeight, ScreeenHalfHeight - 1) - i)
                        )] = grad;
                    }
                    catch
                    {
                        
                    }
                    
                }

                //down
                for (int i = 0; i < wallHeight; i++)
                {
                    try
                    {
                        framebuff[ConvertToFramebuffCoords
                        (left: rayCount
                        , top: (int)ScreeenHalfHeight - ((int)Math.Min(wallHeight, ScreeenHalfHeight - 1)) + ((int)Math.Min(wallHeight, ScreeenHalfHeight - 1) - i)
                        )] = grad;
                    }
                    catch
                    {

                    }
                    
                }
            }
            DrawScreen(framebuff);
            for(int i = 0; i < framebuff.Length; i++)
            {
                framebuff[i] = 0;
            }
        }

        public void Render(GameState gameState)
        {
            GameState = gameState;
            RayCaster();
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
            ScreenHalfWidth = (double) Console.BufferWidth / 2;
            ScreeenHalfHeight = (double) Console.BufferHeight / 2;
            PlayerHalfFov = (double) state.player.FOV / 2;
            RayCasterIncrementAngle = (double) state.player.FOV / ScreenWidth;
            Gradient = " .:-=+*#%@";

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

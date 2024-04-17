using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private double RenderDistance;
        private byte FloorAndCeilingTexture = (byte)' ';

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

        internal void DrawPoint(int left, int top, byte b)
        {
            framebuff[ConvertToFramebuffCoords(left, top)] = b;
        }

        internal void DrawVerticalLines(int left, int topFrom, int topTo, byte b)
        {
            for(int i = 0; i < ScreenHeight; i++)
            {
                DrawPoint(left, i, (i <= topTo && i >= topFrom) ? (byte)b : FloorAndCeilingTexture);
            }
        }
        private void DrawScreen(byte[] framebuff)
        {
            STDOUT.Write(framebuff, 0, framebuff.Length);
            Console.SetCursorPosition(0,0);
        }

        internal double CastRay(double rayAngle)
        {
            double rayX = GameState.player.Pos.x;
            double rayY = GameState.player.Pos.y;

            double rayCos = Math.Cos(DegToRad(rayAngle)) / RayCastingPrecision;
            double raySin = Math.Sin(DegToRad(rayAngle)) / RayCastingPrecision;

            //findig wall
            bool foundWall = false;
            while (!foundWall)
            {
                rayX += rayCos;
                rayY += raySin;

                if (GameState.map.Layout[(int)Math.Floor(rayY), (int)Math.Floor(rayX)] == 1)
                {
                    foundWall = true;
                }

            }

            double playerRayXDelta = GameState.player.Pos.x - rayX;
            double playerRayYDelta = GameState.player.Pos.y - rayY;

            // Pythagoras theorem
            double distance = Math.Sqrt(Math.Pow(playerRayXDelta, 2) + Math.Pow(playerRayYDelta, 2));

            //Fish eye fix
            distance = distance * Math.Cos(DegToRad(rayAngle - GameState.player.Angle));

            return distance; 
        }

        internal void RayCaster()
        {
            double rayAngle = GameState.player.Angle - PlayerHalfFov;

            for(int rayCount = 0; rayCount < ScreenWidth; rayCount++)
            {
                double distance = (double) CastRay(rayAngle);
                int wallHeight = (int) Math.Floor(ScreenHeight / (distance)) ;

                byte gradient = (byte)Gradient[
                    (int) Math.Min(
                            Math.Floor(
                                distance * (Gradient.Length / RenderDistance)
                            )
                            ,Gradient.Length - 1
                        )
                    ];

                DrawVerticalLines(rayCount, (int) ScreeenHalfHeight - wallHeight, (int)ScreeenHalfHeight + wallHeight, gradient);

                rayAngle += RayCasterIncrementAngle;
            }
            int c = 0;
            foreach(char digit in GameState.fps.ToString())
            {
                framebuff[c] = (byte)digit;
                c++;
            }
            DrawScreen(framebuff);
        }

        public void Render(GameState gameState)
        {
            GameState = gameState;
            RayCaster();
        }

        public Renderer(GameState state)
        {
            GameState = state;

            ScreenWidth = Console.BufferWidth;
            ScreenHeight = Console.BufferHeight - 1;
            ScreenHalfWidth = (double) Console.BufferWidth / 2;
            ScreeenHalfHeight = (double) Console.BufferHeight / 2;
            PlayerHalfFov = (double) state.player.FOV / 2;
            RayCasterIncrementAngle = (double)state.player.FOV / ScreenWidth;
            Gradient = "@%#*+=-:. ";
            //Gradient = "0123456789";
            RenderDistance = (GameState.map.Size.x + 2) / 2;

            RayCastingPrecision = 32;

            STDOUT = Console.OpenStandardOutput();

            this.framebuff = new byte[((Console.BufferHeight - 1) * Console.BufferWidth)];
            for(int i = 0; i < framebuff.Length; i++)
            {
                framebuff[i] = (byte)' ';
            }
        }
    }
}

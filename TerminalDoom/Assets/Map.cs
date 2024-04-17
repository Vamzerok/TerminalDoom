using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDoom.Assets
{
    internal class Map
    {
        private string _name;
        public string Name{ get { return _name != ""? _name : "Untitled"; } }

        private Coords _size;
        public Coords Size { get { return _size; } }

        private int[,] _layout;
        public int[,] Layout { get { return _layout; } }

        /*public int GetValueAtRealPosition(double x, double y)
        {
            int matrixLeft = (int)Math.Floor(x + _size.x / 2);
            int matricTop = (int)Math.Floor(y + _size.y / 2);
            return _layout[matricTop, matrixLeft]; //TODO! fix this crap
        }*/
        public Coords GetValueAtRealPosition(double x, double y)
        {
            int matrixLeft = (int)Math.Floor(x + _size.x / 2);
            int matricTop = (int)Math.Floor(y + _size.y / 2);
            return new Coords(matrixLeft, matricTop); //TODO! fix this crap
        }

        public Map(string filePath)
        {
            string[] fileLines = File.ReadAllLines(filePath);

            //store metadata
            string[] metadata = fileLines[0].Split(';');
            _name = metadata[0];
            _size = new Coords(int.Parse(metadata[1].Split('x')[1]), int.Parse(metadata[1].Split('x')[0]));

            //store the layout
            _layout = new int[(int)_size.y, (int)_size.x];
            for(int i = 0; i < _size.y; i++)
            {
                string[] row = fileLines[i+1].Split(';');
                for(int j = 0; j < _size.x; j++)
                {
                    _layout[i, j] = int.Parse(row[j]);
                }
            }
        }
    }
}

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

        public Map(string filePath)
        {
            string[] fileLines = File.ReadAllLines(filePath);

            //store metadata
            string[] metadata = fileLines[0].Split(';');
            _name = metadata[0];
            _size = new Coords(int.Parse(metadata[1].Split('x')[1]), int.Parse(metadata[1].Split('x')[0]));

            //store the layout
            _layout = new int[(int)_size.y, (int)_size.x];
            for(int i = 1; i < fileLines.Length; i++)
            {
                string[] row = fileLines[i].Split(';');
                for(int j = 0; j < row.Length -1; j++)
                {
                    _layout[i-1, j] = int.Parse(row[j]);
                }
            }
        }
    }
}

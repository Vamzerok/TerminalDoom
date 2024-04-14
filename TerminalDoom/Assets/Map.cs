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

        private int _sizeX;
        private int _sizeY;
        public int SizeX {  get { return _sizeX; } }
        public int SizeY { get { return _sizeY; } }

        public Map(string filePath)
        {
            string[] fileLines = File.ReadAllLines(filePath);

            string[] metadata = fileLines[0].Split(';');
            _name = metadata[0];
            _sizeX = int.Parse(metadata[1].Split('x')[0]);
            _sizeX = int.Parse(metadata[1].Split('x')[1]);
        }
    }
}

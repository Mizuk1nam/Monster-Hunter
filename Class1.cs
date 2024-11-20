using System;
using System.Collections.Generic;
using System.IO;

namespace Monster_Hunter
{
    public class Map
    {
        private char[][] mapArray = new char[][] { };
        private int playerX = 0;
        private int playerY = 0;

        // Width and Height variables
        private int _width;
        private int _height;

        // Public properties for Width and Height
        public int Width
        {
            get { return _width; }
            private set
            {
                if (value >= 0 && value <= 75)
                {
                    _width = value;
                }
                else
                {
                    throw new Exception("Width must be between 0 and 75.");
                }
            }
        }

        public int Height
        {
            get { return _height; }
            private set
            {
                if (value >= 0 && value <= 30)
                {
                    _height = value;
                }
                else
                {
                    throw new Exception("Height must be between 0 and 30.");
                }
            }
        }

        // Array to hold .map file names
        public string[] MapFiles { get; private set; }

        // Constructor
        public Map()
        {
            // Find all .map files in the folder
            MapFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.map");
        }

        // Load a map file into MapData
        private void LoadMapFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("The specified map file does not exist.");
            }

            mapArray = new char[][] { }; // Clear any existing map data

            foreach (string fileLine in File.ReadLines(filename))
            {
                char[] fileLineArray = fileLine.ToCharArray();
                Array.Resize(ref mapArray, mapArray.Length + 1);
                mapArray[mapArray.Length - 1] = fileLineArray;
            }

            // Set Width and Height
            Height = mapArray.Length;
            Width = mapArray.Length > 0 ? mapArray[0].Length : 0;

            if (Height > 30 || Width > 75)
            {
                Console.WriteLine("Warning: The map size exceeds the allowed dimensions.");
            }
        }

        // Draw the map to the console
        public void DrawMap()
        {
            for (int y = 0; y < mapArray.Length; y++)
            {
                for (int x = 0; x < mapArray[y].Length; x++)
                {
                    char currentChar = mapArray[y][x];
                    Console.Write(currentChar);

                    if (currentChar == 'P') // Player
                    {
                        playerY = y;
                        playerX = x;
                    }
                }
                Console.WriteLine(); // Line break for the next row
            }
        }
    }
}

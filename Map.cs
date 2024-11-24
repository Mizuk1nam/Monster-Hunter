using System;
using System.Collections.Generic;
using System.IO;

namespace Monster_Hunter
{
    public class Map
    {
        private char[][] mapArray = new char[][] { };

        private int _width;
        private int _height;

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

        public string[] MapFiles { get; private set; }
        public char[][] MapArray => mapArray;

        public Map()
        {
            MapFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.map");
        }

        public void LoadMapFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("The specified map file does not exist.");
            }

            mapArray = new char[][] { };

            foreach (string fileLine in File.ReadLines(filename))
            {
                if (mapArray.Length > 0 && fileLine.Length != mapArray[0].Length)
                {
                    throw new Exception("Inconsistent row lengths in the map file.");
                }

                char[] fileLineArray = fileLine.ToCharArray();
                Array.Resize(ref mapArray, mapArray.Length + 1);
                mapArray[mapArray.Length - 1] = fileLineArray;
            }

            Height = mapArray.Length;
            Width = mapArray.Length > 0 ? mapArray[0].Length : 0;

            if (Height > 30 || Width > 75)
            {
                Console.WriteLine("Warning: The map size exceeds recommended dimensions.");
            }
        }

        public void DrawMap()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Blue;

            for (int Y = 0; Y < mapArray.GetLength(0); Y++)
            {
                for (int X = 0; X < mapArray[Y].Length; X++)
                {
                    Console.Write(mapArray[Y][X]);
                }
                Console.WriteLine();
            }
        }
    }




}

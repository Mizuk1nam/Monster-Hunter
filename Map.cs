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

        // Property to get the width of the map, with validation
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

        // Property to get the height of the map, with validation
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

        // Property to get the map file paths
        public string[] MapFiles { get; private set; }
        // Property to get the map array
        public char[][] MapArray => mapArray;

        // Constructor to initialize map files from the current directory
        public Map()
        {
            MapFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.map");
            if (MapFiles.Length == 0)
            {
                throw new FileNotFoundException("No .map files found in the current directory.");
            }
           
        }

        // Method to load the map from a file and set the dimensions
        public void LoadMapFromFile(string filename)
        {
            // Check if the file exists
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"The file '{filename}' does not exist.");
            }

            // Load map data from the file
            loadMapFromFile(filename);

            // Set the height and width based on the map data
            Height = mapArray.Length;
            Width = mapArray.Length > 0 ? mapArray[0].Length : 0;

            // Ensure map dimensions are within allowed limits
            if (Width > 75 || Height > 30)
            {
                throw new Exception($"Map dimensions exceed limits (Width: {Width}, Height: {Height}).");
            }
        }

        // Method to draw the map to the console
        public void DrawMap()
        {
            // Ensure the map has data before drawing
            if (mapArray.Length == 0)
            {
                throw new InvalidOperationException("Map is empty. Nothing to draw.");
            }

            // Clear the console and set background color
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Blue;

            // Loop through the map array and draw each element
            for (int y = 0; y < mapArray.Length; y++)
            {
                for (int x = 0; x < mapArray[y].Length; x++)
                {
                    Console.Write(mapArray[y][x]);
                }
                Console.WriteLine();
            }

            // Reset console colors
            Console.ResetColor();
        }

        // Private method to load map data from a file into the map array
        private void loadMapFromFile(string filename)
        {
            foreach (string fileLine in File.ReadLines(filename))
            {
                // Convert each line of the file into a char array
                char[] fileLineArray = fileLine.ToCharArray();

                // Resize the map array and add the new line
                Array.Resize(ref mapArray, mapArray.Length + 1);
                mapArray[mapArray.GetUpperBound(0)] = fileLineArray;
            }
        }
       

       
    }




}

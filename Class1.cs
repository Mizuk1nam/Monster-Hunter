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
                Console.WriteLine("Warning: The map size is too big.");
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
                Console.WriteLine();
            }
        }
    }
    public class SingletonRandom
    {
        private static SingletonRandom instance;
        private System.Random random;

        private SingletonRandom()
        {
            random = new System.Random();
        }

        public static SingletonRandom GetInstance()
        {
            if (instance == null)
            {
                instance = new SingletonRandom();
            }
            return instance;
        }

        public int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public double GetRandomDouble()
        {
            return random.NextDouble();
        }

    }
    public class Sword
    {
        // A property for the strength (or offense) of the sword
        public int Strength;

        // Constructor to create a sword with random strength between 4 and 9
        public Sword()
        {
           
            Strength = SingletonRandom.GetInstance().GetRandomNumber(4, 10); // Random value between 4 and 9
        }

        // This method checks if the sword breaks after an attack
        public bool BreakSword()
        {
           
            int chance = SingletonRandom.GetInstance().GetRandomNumber(1, 6);  // Random value between 1 and 5

            // If the random number is 1, the sword breaks
            if (chance == 1)
            {
                Console.WriteLine("The sword broke!");
                return true;
            }
            else
            {
                Console.WriteLine("The sword did not break.");
                return false;
            }
        }
    }
        public class Shield
    {
        //  A property for the armor (or defense) of the shield.
        public int Armor;

        // Create random value of 3-6 when the object is created
        public Shield()
        {
           
            Armor = SingletonRandom.GetInstance().GetRandomNumber(3, 7); // Random value between 3 and 6
        }

        // Checks if the shield breaks after an attack
        public bool BreakShield()
        {
            
            int chance = SingletonRandom.GetInstance().GetRandomNumber(1, 5);  // Random value between 1 and 4

            // If the random number is 1, the shield breaks
            if (chance == 1)
            {
                Console.WriteLine("The shield broke!");
                return true;
            }
            else
            {
                Console.WriteLine("The shield did not break.");
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    using System;

    namespace Monster_Hunter
    {
        public class Monster : Character
        {
            public Monster(int startX, int startY, Map gameMap)
                : base(startX, startY, gameMap)
            {
                FreezeTimeMilliseconds = 2000; // Monster freeze time: 2 seconds
            }

            public override bool Move(int newX, int newY)
            {
                // Check the new position is within map boundaries
                if (newX < 0 || newX >= map.Width || newY < 0 || newY >= map.Height)
                {
                    Console.WriteLine("Move failed: Out of map bounds.");
                    return false;
                }

                // Check if the position is valid (not a wall and not the hunter)
                if (IsValidPosition(newX, newY))
                {
                    // Clear the monster's old position on the map
                    map.MapArray[Y][X] = ' ';

                    // Update monster's position
                    X = newX;
                    Y = newY;

                    // Mark the new position on the map
                    map.MapArray[Y][X] = 'M';
                    return true;
                }

                Console.WriteLine("Move failed");
                return false;
            }

            private bool IsValidPosition(int x, int y)
            {
                // Check if the position is not a wall ('#') and does not contain the hunter ('H')
                return map.MapArray[y][x] != '#' && map.MapArray[y][x] != 'H';
            }

            public void MoveRandomly()
            {
                // Generate random directions to move
                Random random = new Random();
                int newX = X;
                int newY = Y;

                switch (random.Next(4))
                {
                    case 0: newX--; break; // Move left
                    case 1: newX++; break; // Move right
                    case 2: newY--; break; // Move up
                    case 3: newY++; break; // Move down
                }

                if (!Move(newX, newY))
                {
                    Console.WriteLine("Monster couldn't move in that direction. Trying again...");
                }
            }

        }
    }

}

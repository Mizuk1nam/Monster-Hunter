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

            public void MoveRandomly(Map map)
            {
                
                SingletonRandom random = SingletonRandom.GetInstance();
                int newX = this.X;
                int newY = this.Y;
                int attempts = 0;

                // Try up to 5 times to move in a valid direction
                while (attempts < 5)
                {
                    // Pick a random direction (0 = left, 1 = right, 2 = up, 3 = down)
                    switch (random.GetRandomNumber(0, 4))  // Use SingletonRandom's GetRandomNumber
                    {
                        case 0: newX--; break; // Move left
                        case 1: newX++; break; // Move right
                        case 2: newY--; break; // Move up
                        case 3: newY++; break; // Move down
                    }

                    // Try to move to the new position
                    if (Move(newX, newY))  // Use Move() method to check the validity of the new position
                    {
                        return; // If successful, exit
                    }

                    // If the move failed, retry with new random direction
                  
                }

                // If all attempts fail, print a message
                Console.WriteLine("Monster couldn't move after multiple attempts.");
            }

            public void AttackHunter(Hunter hunter)
            {
                // Check if the monster is adjacent to the hunter
                int distanceX = hunter.X - this.X;
                int distanceY = hunter.Y - this.Y;

                if (distanceX <= 1 && distanceY <= 1) // Monster is adjacent to the hunter
                {
                    // Calculate damage
                    int damage = this.Strength - hunter.Armor;

                    // Check if the hunter has a shield
                    if (hunter.Shield != null)
                    {
                        // Add shield armor to the hunter's armor
                        damage -= hunter.Shield.Armor;
                        // Check if the shield breaks after the attack
                        if (hunter.Shield.BreakShield())
                        {
                            hunter.Shield = null; // Shield breaks and is removed
                            Console.WriteLine("The shield has broken!");
                        }
                    }

                    // Inflict damage if it's positive
                    if (damage > 0)
                    {
                        hunter.CurrentHP -= damage;
                        Console.WriteLine($"Hunter takes {damage} damage. Remaining HP: {hunter.CurrentHP}");

                        if (hunter.CurrentHP <= 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"HP: {hunter.CurrentHP}");
                            Console.ResetColor();
                        }

                        if (hunter.IsDead())
                        {
                            Console.WriteLine("The Hunter has died.");
                            AskForNewGame(); // Handle asking for a new game after death
                        }
                    }
                    else
                    {
                        Console.WriteLine("The attack was blocked by armor.");
                    }
                }
            }




            public void AskForNewGame()
            {
                Console.WriteLine("Do you want to play again? (y/n)");
                string response = Console.ReadLine().ToLower();
                if (response == "y")
                {
                    // Logic to reset the game state (e.g., Level 1, Score: 0)
                    StartNewGame();
                }
                else
                {
                    Environment.Exit(0); // Exit the game
                }
            }
           public void StartNewGame()
            {
                // 
            }
            public bool IsAdjacentTo(Hunter hunter)
            {
                int deltaX = this.X - hunter.X;
                int deltaY = this.Y - hunter.Y;

                // Check if both deltas are either -1, 0, or 1 (indicating adjacency)
                return (deltaX >= -1 && deltaX <= 1) && (deltaY >= -1 && deltaY <= 1) && (deltaX != 0 || deltaY != 0);
            }

        }
    }

}

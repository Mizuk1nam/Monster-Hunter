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
        public abstract class Character
        {
            // Position properties
            private int x;
            private int y;

            public int X
            {
                get => x;
                set
                {
                    if ((map != null && value >= 0 && value < map.Width) || value == 0)
                    {
                        x = value;
                    }
                    else
                    {
                        throw new ArgumentException("X is invalid.");
                    }
                }
            }

            public int Y
            {
                get => y;
                set
                {
                    if ((map != null && value >= 0 && value < map.Height) || value == 0)
                    {
                        y = value;
                    }
                    else
                    {
                        throw new ArgumentException("Y position is invalid.");
                    }
                }
            }

            // Maximum HP and current HP
            private int maxHP;
            private int currentHP;

            public int MaxHP
            {
                get => maxHP;
                set
                {
                    if (value > 0 && value <= 30)
                    {
                        maxHP = value;
                    }
                    else
                    {
                        throw new ArgumentException("Max HP must be between 1 and 30.");
                    }
                }
            }

            public int CurrentHP
            {
                get => currentHP;
                set
                {
                    if (value >= 0 && value <= MaxHP)
                    {
                        currentHP = value;
                    }
                    else
                    {
                        throw new ArgumentException("Current HP must be between 0 and Max HP.");
                    }
                }
            }

            // Attack and defense properties
            private int strength;
            private int armor;

            public int Strength
            {
                get => strength;
                set
                {
                    if (value >= 0 && value <= 7)
                    {
                        strength = value;
                    }
                    else
                    {
                        throw new ArgumentException("Strength must be between 0 and 7.");
                    }
                }
            }

            public int Armor
            {
                get => armor;
                set
                {
                    if (value >= 0 && value <= 4)
                    {
                        armor = value;
                    }
                    else
                    {
                        throw new ArgumentException("Armor must be between 0 and 4.");
                    }
                }
            }

            // Freeze time
            public int FreezeTimeMilliseconds { get; set; }

            // Map reference
            private readonly Map map;

            // Constructor
            protected Character(int x, int y, Map map)
            {
                this.map = map;
                X = x;
                Y = y;
            }

            // Abstract move method
            public abstract bool Move(int newX, int newY);

            // Check if the character is dead
            public bool IsDead()
            {
                return CurrentHP <= 0;
            }
        }
    }
}

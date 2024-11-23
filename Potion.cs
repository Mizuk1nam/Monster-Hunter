using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public enum PotionType
    {
        Poisoned,
        Speed,
        Invisibility,
        Strength
    }

    public class Potion
    {
        // Property to hold the type of potion
        public PotionType Type { get; private set; }

        // Constructor
        public Potion()
        {
            // Roll a 6-sided dice to determine potion type
            int diceRoll = SingletonRandom.GetInstance().GetRandomNumber(1, 7); // Random number between 1 and 6

            switch (diceRoll)
            {
                case 1:
                    Type = PotionType.Poisoned;
                    break;
                case 2:
                    Type = PotionType.Speed;
                    break;
                case 3:
                    Type = PotionType.Speed;
                    break;
                case 4:
                    Type = PotionType.Invisibility;
                    break;
                case 5:
                    Type = PotionType.Invisibility;
                    break;
                case 6:
                    Type = PotionType.Strength;
                    break;
            }

            Console.WriteLine($"Potion created: {Type}");
        }


    }
        
}

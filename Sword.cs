using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
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

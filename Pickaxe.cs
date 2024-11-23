using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Pickaxe
    {

        //check if the pickaxe breaks after use
        public bool BreakPickaxe()
        {
           
            int chance = SingletonRandom.GetInstance().GetRandomNumber(1, 4); // Random value between 1 and 3 for pickaxe break chance

            // If the random number is 1, the pickaxe breaks
            if (chance == 1)
            {
                Console.WriteLine("The pickaxe broke!");
                return true;
            }
            else
            {
                Console.WriteLine("The pickaxe did not break.");
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
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
}

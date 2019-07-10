using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public class RandomContext : IRandomContext
    {
        public Random RandomGenerator;

        public RandomContext()
        {
            RandomGenerator = new Random();
        }

        public int GetRandom(int min, int max)
        {
            return RandomGenerator.Next(min, max);
        }
    }
}

using System;

namespace Utils
{
    public static class RandomProvider
    {
        public static IRandomContext Context;

        static RandomProvider()
        {
            Context = new RandomContext();
        }

        public static int GetRandom(int min, int max)
        {
            return Context.GetRandom(min, max + 1);
        }
    }
}

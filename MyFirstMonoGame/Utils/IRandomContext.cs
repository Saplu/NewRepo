using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public interface IRandomContext
    {
        int GetRandom(int min, int max);
    }
}

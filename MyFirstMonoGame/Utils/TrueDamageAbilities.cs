using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public class TrueDamageAbilities
    {
        public static List<string> abilities = new List<string>() { "Fire Within" };

        public static bool IsTrueDmg(string id)
        {
            if (TrueDamageAbilities.abilities.Exists(x => x == id))
                return true;
            else return false;
        }
    }
}

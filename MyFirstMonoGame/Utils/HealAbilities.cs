using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public class HealAbilities
    {
        public static List<string> abilities = new List<string>() { "Heal", "Sacrifice", "Bubble", "Healing Words", "Inspire" };

        public static bool IsHeal(string id)
        {
            if (HealAbilities.abilities.Exists(x => x == id))
                return true;
            else return false;
        }
    }
}

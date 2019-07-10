using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityClassLibrary.Fairy
{
    public class Inspire : Ability
    {
        public Inspire()
        {
            Name = "Inspire";
            Description = "Buff whole party\r\nwith many things\r\nfor 2 turns.\r\nAlso small\r\ninstant heal.";
            Cooldown = 5;
        }

        public int Action(int SpellPower)
        {
            return Convert.ToInt32(SpellPower * .5);
        }
    }
}

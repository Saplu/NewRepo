using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterClassLibrary.Interfaces
{
    interface PlayerInterface
    {
        string[] Ability1();
        string[] Ability2();
        string[] Ability3();
        string[] Ability4();
        int UseAbility(string id);
    }
}

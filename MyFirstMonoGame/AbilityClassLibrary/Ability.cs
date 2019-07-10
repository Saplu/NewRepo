using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityClassLibrary
{
    public abstract class Ability
    {
        private string name;
        private string description;
        private List<int> targets;
        private int cooldown;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public List<int> Targets { get => targets; set => targets = value; }
        public int Cooldown { get => cooldown; set => cooldown = value; }

        public string[] Info()
        {
            var value = new string[2];
            value[0] = this.name;
            value[1] = this.description;
            return value;
        }
    }
}

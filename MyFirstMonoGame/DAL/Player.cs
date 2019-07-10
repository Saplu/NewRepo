using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Player
    {
        private int xp, className, health, strength, spellPower, armor, level;
        private double crit;
        private List<Item> items;
        private List<int> itemTypes;
        private string name;

        public int Xp { get => xp; set => xp = value; }
        public int ClassName { get => className; set => className = value; }
        public int Health { get => health; set => health = value; }
        public int Strength { get => strength; set => strength = value; }
        public int SpellPower { get => spellPower; set => spellPower = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Level { get => level; set => level = value; }
        public double Crit { get => crit; set => crit = value; }
        public List<Item> Items { get => items; set => items = value; }
        public List<int> ItemTypes { get => itemTypes; set => itemTypes = value; }
        public string Name { get => name; set => name = value; }

        public Player(string name, int xp, int className, int health, int strength, int spellPower,
            int armor, int level, double crit, List<Item> items, List<int> itemTypes)
        {
            this.name = name;
            this.xp = xp;
            this.className = className;
            this.health = health;
            this.strength = strength;
            this.spellPower = spellPower;
            this.armor = armor;
            this.level = level;
            this.crit = crit;
            this.items = items;
            this.itemTypes = itemTypes;
        }

        public Player()
        {

        }

        public override string ToString()
        {
            var value = Name + "." + Health + "." + strength + "." + spellPower + "." +
                crit + "." + Armor + "." + level + "." + xp + "." + className + "\r\n";
            return value;
        }
    }
}

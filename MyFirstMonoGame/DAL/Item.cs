using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Item
    {
        private int health, strength, spellPower, armor, sellValue, itemType, itemPlace, itemQuality;
        private double crit;
        private string name, owner;

        public int Health { get => health; set => health = value; }
        public int Strength { get => strength; set => strength = value; }
        public int SpellPower { get => spellPower; set => spellPower = value; }
        public int Armor { get => armor; set => armor = value; }
        public int SellValue { get => sellValue; set => sellValue = value; }
        public int ItemType { get => itemType; set => itemType = value; }
        public int ItemPlace { get => itemPlace; set => itemPlace = value; }
        public int ItemQuality { get => itemQuality; set => itemQuality = value; }
        public double Crit { get => crit; set => crit = value; }
        public string Name { get => name; set => name = value; }
        public string Owner { get => owner; set => owner = value; }

        public Item(int health, int strength, int spellPower, int armor, int sellValue, int itemType, int itemPlace, int itemQuality,
            double crit, string name, string owner)
        {
            this.health = health;
            this.strength = strength;
            this.spellPower = spellPower;
            this.armor = armor;
            this.sellValue = sellValue;
            this.itemType = itemType;
            this.itemPlace = itemPlace;
            this.itemQuality = itemQuality;
            this.crit = crit;
            this.name = name;
            this.owner = owner;
        }

        public Item()
        {

        }

        public override string ToString()
        {
            var value = name + "." + health + "." + strength + "." + spellPower + "." +
                crit + "." + itemType + "." + armor + "." + itemPlace + "." + sellValue + "." +
                ItemQuality + "\r\n";
            return value;
        }
    }
}

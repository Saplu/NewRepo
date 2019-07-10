using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary.Enums;

namespace CharacterClassLibrary
{
    [Serializable]
    public class Item
    {
        private int health;
        private int strength;
        private double crit;
        private int spellpower;
        private int armor;
        private string name;
        private int level;
        private ItemType itemType;
        private ItemPlace itemPlace;
        private string owner;
        private int sellValue;
        private ItemQuality quality;

        public int Health { get => health; set => health = value; }
        public int Strength { get => strength; set => strength = value; }
        public double Crit { get => crit; set => crit = value; }
        public int Spellpower { get => spellpower; set => spellpower = value; }
        public int Armor { get => armor; set => armor = value; }
        public string Name { get => name; set => name = value; }
        public int Level { get => level; set => level = value; }
        public ItemType ItemType { get => itemType; set => itemType = value; }
        public ItemPlace ItemPlace { get => itemPlace; set => itemPlace = value; }
        public string Owner { get => owner; set => owner = value; }
        public int SellValue { get => sellValue; set => sellValue = value; }
        public ItemQuality Quality { get => quality; set => quality = value; }

        public Item(int health, int strength, double crit, int spellPower, int armor, string name, int level,
            ItemType itemtype, ItemPlace itemplace, string owner, ItemQuality quality)
        {
            Health = health;
            Strength = strength;
            Crit = crit;
            Spellpower = spellPower;
            Armor = armor;
            Name = name;
            Level = level;
            ItemType = itemtype;
            ItemPlace = itemplace;
            Owner = owner;
            Quality = quality;
            var value = new RandomItemGenerator();
            SellValue = value.GetItemPower(ItemPlace, Level, Quality) * 2;
        }

        public Item(int level)
        {
            Level = level;
        }

        public Item()
        {

        }

        public override string ToString()
        {
            return name + "\r\nHealth: " + health + "\r\nStrength: " + strength + "\r\nSpellpower: " + spellpower +
                "\r\nArmor: " + armor + "\r\nCrit: " + crit + "\r\n\r\n" + itemType.ToString() + " " + itemPlace.ToString() + 
                " of " + Quality.ToString() + " quality.\r\nSell value: " + SellValue;
        }

        public string ToString(int thing)
        {
            return itemType.ToString() + " " + itemPlace.ToString() +
                " of " + Quality.ToString() + " quality.\r\nStats will be random because the\r\nlack of craftsmanshipskill\r\nCost: "
                + SellValue * 4;
        }
    }
}

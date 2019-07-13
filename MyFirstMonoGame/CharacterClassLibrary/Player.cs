using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary.Enums;

namespace CharacterClassLibrary
{
    [Serializable]
    public abstract class Player : Character
    {
        private int xp;
        private string name;
        private Enums.ClassName className;
        private List<Item> items;
        private List<ItemType> itemTypes;
        private int[] cooldowns;
        private List<ItemPlace> itemPlaces = new List<ItemPlace>(){ItemPlace.MainHand, ItemPlace.Head,
            ItemPlace.Chest, ItemPlace.Hands, ItemPlace.Legs, ItemPlace.Feet };

        public int Xp { get => xp; set => xp = value; }
        public string Name { get => name; set => name = value; }
        public ClassName ClassName { get => className; set => className = value; }
        public List<Item> Items { get => items; set => items = value; }
        public List<ItemType> ItemTypes { get => itemTypes; set => itemTypes = value; }
        public int[] Cooldowns { get => cooldowns; set => cooldowns = value; }
        public List<ItemPlace> ItemPlaces { get => itemPlaces; set => itemPlaces = value; }

        public static Player Create(ClassName className)
        {
            switch(className)
            {
                case ClassName.Warrior: return new PlayerClasses.Warrior("asd");
                case ClassName.Mage: return new PlayerClasses.Mage("asd");
                case ClassName.BloodPriest: return new PlayerClasses.BloodPriest("asd");
                case ClassName.Protector: return new PlayerClasses.Protector("asd");
                case ClassName.Fairy: return new PlayerClasses.Fairy("asd");
                case ClassName.Shaman: return new PlayerClasses.Shaman("asd");
                case ClassName.Rogue: return new PlayerClasses.Rogue("asd");
                case ClassName.Templar: return new PlayerClasses.Templar("asd");
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public abstract string[] Ability1();
        public abstract string[] Ability2();
        public abstract string[] Ability3();
        public abstract string[] Ability4();

        public virtual int GetThreat(string id)
        {
            return 0;
        }

        public void ModifyCooldownLength()
        {
            var newcd = new int[4];
            for (int i = 0; i < 4; i++)
            {
                cooldowns[i]--;
                if (cooldowns[i] > 0)
                    newcd[i] = cooldowns[i];
            }
            Cooldowns = newcd;
        }

        public virtual void AfterCombatReset()
        {
            Health = MaxHealth;
            Cooldowns = new int[4] { 0, 0, 0, 4 };
            Statuses = new List<CombatLogicClassLibrary.Status>();
        }

        public List<string> AbilityInfo()
        {
            var info = new List<string>();
            info.Add(Ability1()[1]);
            info.Add(Ability2()[1]);
            info.Add(Ability3()[1]);
            info.Add(Ability4()[1]);
            return info;
        }

        public void CheckForLevelUp()
        {
            if (xp >= Level * 100)
            {
                xp -= Level * 100;
                Level++;
                var info = Convert.ToInt32(className);
                switch (info)
                {
                    case 0: modifyPlayer(20, 2, 0); break;
                    case 1: modifyPlayer(15, 0, 2); break;
                    case 2: modifyPlayer(17, 0, 2); break;
                    case 3: modifyPlayer(24, 2, 0); break;
                    case 4: modifyPlayer(15, 0, 2); break;
                    case 5: modifyPlayer(18, 0, 2); break;
                    case 6: modifyPlayer(17, 2, 0); break;
                    case 7: modifyPlayer(24, 0, 2); break;
                }
            }
        }

        public void AddItem(Item item)
        {
            if (itemPlaces.Exists(x => x == item.ItemPlace))
            {
                if (Items.Exists(x => x.ItemPlace == item.ItemPlace))
                    removeItem(Convert.ToInt32(item.ItemPlace));
                addItem(item);
            }
            else throw new Exception("Cannot wear the armor type.");
        }

        public int RemovedItemValue(Item loot)
        {
            var itemPlace = items.IndexOf(items.Find(x => x.ItemPlace == loot.ItemPlace));
            return items[itemPlace].SellValue;
        }

        private void modifyPlayer(int health, int strength, int spellPower)
        {
            Health += health;
            Strength += strength;
            SpellPower += spellPower;
        }

        private void removeItem(int itemPlace)
        {
            Health -= items[itemPlace].Health;
            Strength -= items[itemPlace].Strength;
            SpellPower -= items[itemPlace].Spellpower;
            Crit -= items[itemPlace].Crit;
            Armor -= items[itemPlace].Armor;
        }

        private void addItem(Item item)
        {
            items[Convert.ToInt32(item.ItemPlace)] = item;
            Health += item.Health;
            Strength += item.Strength;
            SpellPower += item.Spellpower;
            Crit += item.Crit;
            Armor += item.Armor;
        }
    }
}

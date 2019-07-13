using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary.Enums;

namespace CharacterClassLibrary
{
    [Serializable]
    public class RandomItemGenerator
    {
        public Item CreateItem(int level, ItemQuality quality)
        {
            var item = new Item(level);
            var type = casterOrPhysical();
            item.Quality = quality;
            item.ItemType = (ItemType)Enum.Parse(typeof(ItemType), chooseItemType(type).ToString());
            item.ItemPlace = (ItemPlace)Enum.Parse(typeof(ItemPlace), chooseItemPlace().ToString());
            if (Convert.ToInt32(item.ItemPlace) == 0 || Convert.ToInt32(item.ItemPlace) == 1)
                item.ItemType = ItemType.Cloth;
            if (Convert.ToInt32(item.ItemPlace) == 7)
                item.ItemType = ItemType.Mail;
            item.Armor = getItemArmor(item.ItemType, item.ItemPlace, level, item.Quality);
            var usablePower = GetItemPower(item.ItemPlace, item.Level, item.Quality);
            spreadPower(usablePower, item, type);
            item.Name = randomName();
            item.SellValue = usablePower * 2;

            return item;
        }

        public Item CreateItem(int level, int place, int type, int casterOrPhysical, string name, ItemQuality quality)
        {
            var item = new Item(level);
            item.Quality = quality;
            item.ItemType = (ItemType)Enum.Parse(typeof(ItemType), type.ToString());
            item.ItemPlace = (ItemPlace)Enum.Parse(typeof(ItemPlace), place.ToString());
            if (Convert.ToInt32(item.ItemPlace) == 0 || Convert.ToInt32(item.ItemPlace) == 1)
                item.ItemType = ItemType.Cloth;
            if (Convert.ToInt32(item.ItemPlace) == 7)
                item.ItemType = ItemType.Mail;
            item.Armor = getItemArmor(item.ItemType, item.ItemPlace, level, item.Quality);
            var usablePower = GetItemPower(item.ItemPlace, item.Level, item.Quality);
            spreadPower(usablePower, item, casterOrPhysical);
            item.Name = randomName();
            item.SellValue = usablePower * 2;
            item.Owner = name;
            return item;
        }

        public int GetItemPower(ItemPlace itemPlace, int level, ItemQuality quality)
        {
            var value = Convert.ToInt32(itemPlace);
            double qualityMultiplier = getQuality(quality);
            var power = 0;
            switch (value)
            {
                case 0: power = level * 5; break;
                case 1: power = level * 3; break;
                case 2: power = level * 3; break;
                case 3: power = level * 4; break;
                case 4: power = level * 2; break;
                case 5: power = level * 4; break;
                case 6: power = level * 2; break;
                case 7: power = level * 2; break;
                default: throw new ArgumentOutOfRangeException();
            }
            return Convert.ToInt32(power * qualityMultiplier);
        }

        private double getQuality(ItemQuality quality)
        {
            switch(Convert.ToInt32(quality))
            {
                case 0: return .5;
                case 1: return 1;
                case 2: return 1.3;
                case 3: return 1.6;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private int casterOrPhysical()
        {
            var type = Utils.RandomProvider.GetRandom(0, 1);
            return type;
        }

        private int chooseItemType(int type)
        {
            if (type == 0)
            {
                var rand = Utils.RandomProvider.GetRandom(0, 4);
                if (rand == 4)
                    return 0;
                else return rand;
            }
            else return Utils.RandomProvider.GetRandom(1, 3);
        }

        private int chooseItemPlace()
        {
            var place = Utils.RandomProvider.GetRandom(0, 6);
            if (place == 1)
            {
                var type = Utils.RandomProvider.GetRandom(0, 1);
                switch (type)
                {
                    case 0: return 1;
                    case 1: return 7;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            else return place;
        }


        private int getItemArmor(ItemType itemType, ItemPlace itemPlace, int level, ItemQuality quality)
        {
            var type = Convert.ToInt32(itemType) + 1;
            var qualityMultiplier = getQuality(quality);
            var place = Convert.ToInt32(itemPlace);
            var armor = 0;
            switch (place)
            {
                case 0: return armor;
                case 1: return armor;
                case 2: armor = Convert.ToInt32(type * level * 1.2); break;
                case 3: armor = type * level * 2; break;
                case 4: armor = Convert.ToInt32(type * level * .8); break;
                case 5: armor = Convert.ToInt32(type * level * 1.5); break;
                case 6: armor = type * level; break;
                case 7: armor = Convert.ToInt32(type * level * 2.5); break;
                default: throw new ArgumentOutOfRangeException();
            }
            return Convert.ToInt32(armor * qualityMultiplier);
        }

        private void spreadPower(int usablePower, Item item, int type)
        {
            if (type == 0)
                item.Strength = 0;
            else if (type == 1)
                item.Spellpower = 0;
            switch (Convert.ToInt32(item.ItemPlace))
            {
                case 0: spreadWeaponPower(usablePower, item, type);
                    break;
                case 1: spreadWeaponPower(usablePower, item, type);
                    break;
                case 8: spreadWeaponPower(usablePower, item, type);
                    break;
                case 7: spreadShieldPower(usablePower, item, type);
                    break;
                default: spreadArmorPower(usablePower, item, type);
                    break;
            }
        }

        private void spreadWeaponPower(int usablePower, Item item, int type)
        {
            if (type == 0)
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 3: item.Crit++; break;
                        default: item.Spellpower++; break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 1: item.Crit++; break;
                        case 2: item.Health += 7; break;
                        default: item.Strength++; break;
                    }
                }
            }
        }

        private void spreadShieldPower(int usablePower, Item item, int type)
        {
            if (type == 0)
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 3);
                    switch(value)
                    {
                        case 1: item.Spellpower++; break;
                        case 2: item.Crit += .8; break;
                        case 3: item.Health += 8; break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 1: item.Strength++; break;
                        default: item.Health += 10; break;
                    }
                }
            }
        }

        private void spreadArmorPower(int usablePower, Item item, int type)
        {
            if (type == 0 && Convert.ToInt32(item.ItemType) == 3)
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 3);
                    switch(value)
                    {
                        case 1: item.Spellpower++; break;
                        default: item.Health += 10; break;
                    }
                }
            }

            else if (type == 1 && Convert.ToInt32(item.ItemType) == 3)
            {
                for(int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 3);
                    switch(value)
                    {
                        case 1: item.Strength++; break;
                        default: item.Health += 10; break;
                    }
                }
            }

            else if (type == 0)
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 1: item.Health += 8; break;
                        case 2: item.Crit += .6; break;
                        default: item.Spellpower++; break;
                    }
                }
            }

            else
            {
                for (int i = 0; i < usablePower; i++)
                {
                    var value = Utils.RandomProvider.GetRandom(1, 4);
                    switch(value)
                    {
                        case 1: item.Health += 9; break;
                        case 2: item.Crit += .6; break;
                        default: item.Strength++; break;
                    }
                }
            }
        }

        private string randomName()
        {
            var nameList = new List<string>() { "Unicorn", "Rainbow", "Marshmallow", "Bunny" };
            var random = Utils.RandomProvider.GetRandom(0, nameList.Count - 1);
            return nameList[random];
        }
    }
}

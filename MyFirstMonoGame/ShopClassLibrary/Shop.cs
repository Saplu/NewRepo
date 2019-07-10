using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using CharacterClassLibrary;

namespace ShopClassLibrary
{
    public class Shop
    {
        private List<CharacterClassLibrary.Player> players;
        private int money;
        private List<CharacterClassLibrary.Item> items;
        private CharacterClassLibrary.Player selectedPlayer;
        private DAO dao;
        private CharacterClassLibrary.Item offer;

        public List<CharacterClassLibrary.Player> Players { get => players; set => players = value; }
        public int Money { get => money; set => money = value; }
        public List<CharacterClassLibrary.Item> Items { get => items; set => items = value; }
        public CharacterClassLibrary.Player SelectedPlayer { get => selectedPlayer; set => selectedPlayer = value; }
        public DAO Dao { get => dao; set => dao = value; }
        public CharacterClassLibrary.Item Offer { get => offer; set => offer = value; }

        public Shop(List<CharacterClassLibrary.Player> players, DAO dao)
        {
            Dao = dao;
            Players = players;
            Money = dao.Party.Money;
            Items = new List<CharacterClassLibrary.Item>();
            selectedPlayer = players[0];
        }

        public int ManageType(int place, int type)
        {
            if (place == 0 || place == 1)
                type = 0;
            if (place == 7)
                type = 2;
            return type;
        }

        public void GenerateOffer(string id)
        {
            var place = setItemPlace(id);
            var type = setItemType();
            var caster = casterOrPhysical();
            var generator = new RandomItemGenerator();
            offer = generator.CreateItem(selectedPlayer.Level, place, type, caster, selectedPlayer.Name,
                CharacterClassLibrary.Enums.ItemQuality.Good);
        }

        private int casterOrPhysical()
        {
            if (selectedPlayer.ClassName == CharacterClassLibrary.Enums.ClassName.BloodPriest ||
                selectedPlayer.ClassName == CharacterClassLibrary.Enums.ClassName.Fairy ||
                selectedPlayer.ClassName == CharacterClassLibrary.Enums.ClassName.Mage ||
                selectedPlayer.ClassName == CharacterClassLibrary.Enums.ClassName.Templar)
                return 0;
            else return 1;
        }

        private int setItemType()
        {
            switch(selectedPlayer.ClassName)
            {
                case CharacterClassLibrary.Enums.ClassName.BloodPriest: return 1;
                case CharacterClassLibrary.Enums.ClassName.Fairy: return 0;
                case CharacterClassLibrary.Enums.ClassName.Mage: return 0;
                case CharacterClassLibrary.Enums.ClassName.Protector: return 3;
                case CharacterClassLibrary.Enums.ClassName.Rogue: return 1;
                case CharacterClassLibrary.Enums.ClassName.Shaman: return 2;
                case CharacterClassLibrary.Enums.ClassName.Templar: return 3;
                case CharacterClassLibrary.Enums.ClassName.Warrior: return 2;
                default: return 0;
            }
        }

        public string CurrentOffer(int type, int place, CharacterClassLibrary.Player buyer)
        {
            var typeString = offerString(type);
            var Place = placeString(place);
            var value = new RandomItemGenerator();
            var placeEnum = (CharacterClassLibrary.Enums.ItemPlace)Enum.Parse(typeof(CharacterClassLibrary.Enums.ItemPlace), place.ToString());
            var price = value.GetItemPower(placeEnum, buyer.Level, CharacterClassLibrary.Enums.ItemQuality.Good) * 8;
            var offer = "Level " + buyer.Level + " " + typeString + " " + Place + 
                "\r\nPrice: " + price + ". Don't bother bargaining, I am computer.";
            return offer;
        }

        private void typeValid(int type)
        {
            if ((Convert.ToInt32(selectedPlayer.ClassName) == 1 || Convert.ToInt32(
                selectedPlayer.ClassName) == 4) && type > 0)
                throw new Exception("Target cannot use the armor type");
            if ((Convert.ToInt32(selectedPlayer.ClassName) == 2 || Convert.ToInt32(
                selectedPlayer.ClassName) == 6) && type > 1)
                throw new Exception("Target cannot use the armor type");
            if ((Convert.ToInt32(selectedPlayer.ClassName) == 0 || Convert.ToInt32(
                selectedPlayer.ClassName) == 5) && type > 2)
                throw new Exception("target Cannot use the armor type");
        }
        
        private int setItemPlace(string id)
        {
            switch(id)
            {
                case "Helmet": return 2;
                case "Chest": return 3;
                case "Hands": return 4;
                case "Legs": return 5;
                case "Feet": return 6;
                default: return 0;
            }
        }

        private string offerString(int type)
        {
            switch(type)
            {
                case 0: return "Cloth";
                case 1: return "Leather";
                case 2: return "Mail";
                case 3: return "Plate";
                default: throw new Exception("Something weird has happened. Please contact our customer support.");
            }
        }

        private string placeString(int place)
        {
            switch(place)
            {
                case 0: return "Main Hand";
                case 1: return "Offhand";
                case 2: return "Helmet";
                case 3: return "Chest";
                case 4: return "Gloves";
                case 5: return "Pants";
                case 6: return "Shoes";
                case 7: return "Shield";
                default: throw new Exception("Something weird has happened. Please contact our customer support.");
            }
        }
    }
}

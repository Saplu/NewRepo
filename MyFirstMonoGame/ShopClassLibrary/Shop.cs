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
        private CharacterClassLibrary.Party party;
        //private List<CharacterClassLibrary.Player> players;
        //private int money;
        private List<CharacterClassLibrary.Item> items;
        private CharacterClassLibrary.Player selectedPlayer;
        private DAO dao;
        private CharacterClassLibrary.Item offer;

        //public List<CharacterClassLibrary.Player> Players { get => players; set => players = value; }
        //public int Money { get => money; set => money = value; }
        public List<CharacterClassLibrary.Item> Items { get => items; set => items = value; }
        public CharacterClassLibrary.Player SelectedPlayer { get => selectedPlayer; set => selectedPlayer = value; }
        public DAO Dao { get => dao; set => dao = value; }
        public CharacterClassLibrary.Item Offer { get => offer; set => offer = value; }
        public CharacterClassLibrary.Party Party { get => party; set => party = value; }

        public Shop(CharacterClassLibrary.Party party, DAO dao)
        {
            Dao = dao;
            this.party = party;
            //Money = dao.Party.Money;
            Items = new List<CharacterClassLibrary.Item>();
            selectedPlayer = party.Players[0];
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

        public CharacterClassLibrary.Party PurcasheItem()
        {
            if (offer != null)
            {
                if (party.Money < offer.SellValue * 4)
                    throw new Exception("You cannot afford that. LoL.");
                party.Money -= offer.SellValue * 4;
                var currentIndex = selectedPlayer.Items.IndexOf(selectedPlayer.Items.Find(x => x.ItemPlace == offer.ItemPlace));
                Party.Money += selectedPlayer.Items[currentIndex].SellValue;
                selectedPlayer.AddItem(offer);
                var replaceIndex = party.Players.IndexOf(party.Players.Find(x => x.Name == selectedPlayer.Name));
                party.Players[replaceIndex] = selectedPlayer;
                return party;
            }
            else throw new Exception("Select some item first.");
        }

        public void SaveChanges(DAL.Party dalParty)
        {
            dao.Update(dalParty);
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

        private int setItemPlace(string id)
        {
            switch (id)
            {
                case "Helmet": return 2;
                case "Chest": return 3;
                case "Hands": return 4;
                case "Legs": return 5;
                case "Feet": return 6;
                default: return 0;
            }
        }
    }
}

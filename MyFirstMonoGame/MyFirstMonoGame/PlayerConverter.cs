using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterClassLibrary;
using CharacterClassLibrary.Enums;
using DAL;

namespace MyFirstMonoGame
{
    public class PlayerConverter
    {
        public PlayerConverter()
        {

        }

        public List<CharacterClassLibrary.Player> DAOToGame(List<DAL.Player> DAOPlayers)
        {
            var players = new List<CharacterClassLibrary.Player>();
            foreach (var player in DAOPlayers)
            {
                var newPlayer = generatePlayer(player);
                players.Add(newPlayer);
            }
            return players;
        }

        public DAL.Party GameToDAO(List<CharacterClassLibrary.Player> players)
        {
            var list = new List<DAL.Player>();
            foreach (var player in players)
            {
                var DAOPlayer = new DAL.Player();
                DAOPlayer.Name = player.Name;
                DAOPlayer.Health = player.Health;
                DAOPlayer.Strength = player.Strength;
                DAOPlayer.SpellPower = player.SpellPower;
                DAOPlayer.Crit = player.Crit;
                DAOPlayer.Armor = player.Armor;
                DAOPlayer.Level = player.Level;
                DAOPlayer.Xp = player.Xp;
                DAOPlayer.Items = convertItems(player.Items);
                DAOPlayer.ItemTypes = convertItemTypes(player.ItemTypes);
                DAOPlayer.ClassName = Convert.ToInt32(player.ClassName);
                list.Add(DAOPlayer);
            }
            return new Party(list);
        }

        private List<DAL.Item> convertItems(List<CharacterClassLibrary.Item> items)
        {
            var list = new List<DAL.Item>();
            foreach (var item in items)
            {
                var DALItem = new DAL.Item();
                DALItem.Name = item.Name;
                DALItem.Owner = item.Owner;
                DALItem.Health = item.Health;
                DALItem.Strength = item.Strength;
                DALItem.SpellPower = item.Spellpower;
                DALItem.Crit = item.Crit;
                DALItem.Armor = item.Armor;
                DALItem.SellValue = item.SellValue;
                DALItem.ItemPlace = Convert.ToInt32(item.ItemPlace);
                DALItem.ItemQuality = Convert.ToInt32(item.Quality);
                DALItem.ItemType = Convert.ToInt32(item.ItemType);
                list.Add(DALItem);
            }
            return list;
        }

        private List<int> convertItemTypes(List<ItemType> itemTypes)
        {
            var list = new List<int>();
            foreach (var item in itemTypes)
            {
                list.Add(Convert.ToInt32(item));
            }
            return list;
        }

        private CharacterClassLibrary.Player generatePlayer(DAL.Player DAOPlayer)
        {
            var playerClass = (ClassName)Enum.Parse(typeof(ClassName), DAOPlayer.ClassName.ToString());
            var player = CharacterClassLibrary.Player.Create(playerClass);
            player.Name = DAOPlayer.Name;
            player.Health = DAOPlayer.Health;
            player.MaxHealth = player.Health;
            player.Strength = DAOPlayer.Strength;
            player.SpellPower = DAOPlayer.SpellPower;
            player.Crit = DAOPlayer.Crit;
            player.Armor = DAOPlayer.Armor;
            player.Level = DAOPlayer.Level;
            player.Xp = DAOPlayer.Xp;
            player.Items = convertItems(DAOPlayer.Items);
            return player;
        }

        private List<CharacterClassLibrary.Item> convertItems(List<DAL.Item> items)
        {
            var list = new List<CharacterClassLibrary.Item>();
            foreach (var item in items)
            {
                var CItem = new CharacterClassLibrary.Item();
                CItem.Name = item.Name;
                CItem.Owner = item.Owner;
                CItem.Health = item.Health;
                CItem.Strength = item.Strength;
                CItem.Spellpower = item.SpellPower;
                CItem.Crit = item.Crit;
                CItem.Armor = item.Armor;
                CItem.SellValue = item.SellValue;
                CItem.ItemPlace = (ItemPlace)Enum.Parse(typeof(ItemPlace), item.ItemPlace.ToString());
                CItem.ItemType = (ItemType)Enum.Parse(typeof(ItemType), item.ItemType.ToString());
                CItem.Quality = (ItemQuality)Enum.Parse(typeof(ItemQuality), item.ItemQuality.ToString());
                list.Add(CItem);
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstMonoGame.Presentation
{
    public class VictoryView
    {
        Texture2D backGround;
        Label infoLabel, lootLabel, currentLabel;
        MissionClassLibrary.SuccessfulMission mission;
        Button okButton;
        List<Button> playerButtons;
        string selectedPlayer;
        List<Player> survivors, players;
        ItemDAO itemDAO;
        CharacterDAO characterDAO;
        Hero hero;
        Map map;

        public VictoryView(Texture2D backGround, MissionClassLibrary.SuccessfulMission mission, 
            Texture2D buttonTexture, SpriteFont font, Hero hero, Map map)
        {
            itemDAO = new ItemDAO();
            characterDAO = new CharacterDAO();
            this.backGround = backGround;
            this.mission = mission;
            survivors = characterDAO.GetSurvivors(mission.Survivors);
            players = characterDAO.GetPlayers(mission.Players);
            infoLabel = new Label("info", font, "Grats!\r\nYour survivors gained " + mission.Xp + " xp!", 200, 50, 300, 40);
            lootLabel = new Label("loot", font, "Loot:\r\n" + mission.Loot.ToString(), 100, 120, 300, 120);
            currentLabel = new Label("current", font, "Currently wearing:\r\n" + currentItem(), 600, 120, 200, 120);
            selectedPlayer = "Inventory";
            okButton = new Button(buttonTexture, 550, 410, "Great, continue!", 160, 50);

            playerButtons = new List<Button>();
            foreach (var player in mission.Mission.Players)
            {
                var texture = buttonTexture;
                var buttonX = 360;
                var buttonY = 120 + (mission.Mission.Players.IndexOf(player) * 65);
                var button = new Button(texture, buttonX, buttonY, player.Name, 80, 50);
                playerButtons.Add(button);
            }
            this.hero = hero;
            this.map = map;
        }



        public void Draw(SpriteBatch sprite, SpriteFont font)
        {
            sprite.Draw(backGround, new Rectangle(0, 0, 800, 480), Color.White);
            infoLabel.Draw(sprite, font);
            lootLabel.Draw(sprite, font);
            currentLabel.Draw(sprite, font);
            foreach (var button in playerButtons)
                button.Draw(sprite, font);
            okButton.Draw(sprite, font);
        }

        public void UpdateButtons(MouseState currentState)
        {
            foreach (var button in playerButtons)
                button.UpdateMouseState(currentState);
            okButton.UpdateMouseState(currentState);
        }

        public string CheckButtons()
        {
            foreach (var button in playerButtons)
            {
                if (button.ButtonClicked())
                {
                    selectedPlayer = button.Text;
                    currentLabel.Text = "Currently wearing:\r\n" + currentItem();
                }
            }
            if (okButton.ButtonClicked())
            {
                var target = mission.Mission.Players.Find(x => x.Name == selectedPlayer);
                try
                {
                    if (selectedPlayer == "Inventory")
                    {
                        characterDAO.ModifyXp(survivors, mission.Xp);
                        mission.Loot.Owner = selectedPlayer;
                        var reward = convertToDbItem(mission.Loot);
                        itemDAO.AddNewItem(reward);
                        map.RemoveDestroyedEnemy(hero);
                        return "Adventure";
                    }
                    else if (target.ItemTypes.Exists(x => x == mission.Loot.ItemType))
                    {
                        itemDAO.removeCurrentItem(Convert.ToInt32(mission.Loot.ItemPlace), selectedPlayer);
                        characterDAO.ModifyXp(survivors, mission.Xp);
                        mission.Loot.Owner = selectedPlayer;
                        var reward = convertToDbItem(mission.Loot);
                        itemDAO.AddNewItem(reward);
                        map.RemoveDestroyedEnemy(hero);
                        return "Adventure";
                    }
                    else throw new Exception("Cannot wear the armor type.");
                }
                catch (Exception ex)
                {
                    currentLabel.Text = ex.Message;
                }
            }
            return "Victory";
        }

        private string currentItem()
        {
            var items = itemDAO.GetItems();
            var loot = mission.Loot;
            if (items.Exists(x => x.Owner == selectedPlayer && x.Place == Convert.ToInt32(loot.ItemPlace)))
            {
                var item = items.Find(x => x.Owner == selectedPlayer &&
                x.Place == Convert.ToInt32(loot.ItemPlace));
                return itemDAO.itemToString(item);
            }
            else return "Nothing equipped.";
        }

        private Item convertToDbItem(CharacterClassLibrary.Item item)
        {
            var dbItem = new Item();
            dbItem.Armor = item.Armor;
            dbItem.Crit = item.Crit;
            dbItem.Health = item.Health;
            dbItem.Owner = item.Owner;
            dbItem.Place = Convert.ToInt32(item.ItemPlace);
            dbItem.SpellPower = item.Spellpower;
            dbItem.Strength = item.Strength;
            dbItem.Type = Convert.ToInt32(item.ItemType);
            dbItem.Name = item.Name;
            dbItem.SellValue = item.SellValue;
            dbItem.Quality = Convert.ToInt32(item.Quality);
            return dbItem;
        }
    }
}

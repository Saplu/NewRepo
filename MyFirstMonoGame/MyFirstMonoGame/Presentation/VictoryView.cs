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
        Hero hero;
        Map map;

        public VictoryView(Texture2D backGround, MissionClassLibrary.SuccessfulMission mission,
            Texture2D buttonTexture, SpriteFont font, Hero hero, Map map)
        {
            this.backGround = backGround;
            this.mission = mission;
            infoLabel = new Label("info", font, "Grats!\r\nYour survivors gained " + mission.Xp + " xp!", 200, 50, 300, 40);
            lootLabel = new Label("loot", font, "Loot:\r\n" + mission.Loot.ToString(), 100, 120, 300, 120);
            currentLabel = new Label("current", font, "Currently wearing:\r\n" + currentItem(0), 600, 120, 200, 120);
            selectedPlayer = "Sell it";
            okButton = new Button(buttonTexture, 550, 410, "Great, continue!", 160, 50);

            playerButtons = new List<Button>();
            foreach (var player in mission.Mission.Players)
            {
                var texture = buttonTexture;
                var buttonX = 360;
                var buttonY = 100 + (mission.Mission.Players.IndexOf(player) * 55);
                var button = new Button(texture, buttonX, buttonY, player.Name, 80, 50);
                playerButtons.Add(button);
            }
            var inventoryButton = new Button(buttonTexture, 360, 320, "Sell it", 80, 50);
            playerButtons.Add(inventoryButton);
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
                    if (selectedPlayer != "Sell it")
                        currentLabel.Text = "Currently wearing:\r\n" + currentItem(playerButtons.IndexOf(button));
                    else currentLabel.Text = "Selling it for full value!";
                }
            }
            if (okButton.ButtonClicked())
            {
                var target = mission.Mission.Players.Find(x => x.Name == selectedPlayer);
                try
                {
                    if (selectedPlayer == "Sell it")
                    {
                        mission.ModifyXp();
                        mission.Loot.Owner = selectedPlayer;
                        map.RemoveDestroyedEnemy(hero);
                        mission.GoldReward = mission.Loot.SellValue;
                        return "Adventure";
                    }
                    else if (target.ItemTypes.Exists(x => x == mission.Loot.ItemType))
                    {
                        mission.ModifyXp();
                        mission.Loot.Owner = selectedPlayer;
                        var reward = mission.Loot;
                        mission.GoldReward = target.RemovedItemValue(mission.Loot);
                        target.AddItem(reward);
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

        public int GetReward()
        {
            return mission.GoldReward;
        }

        private string currentItem(int index)
        {
            var items = mission.Mission.Players[index].Items;
            var loot = mission.Loot;
            if (items.Exists(x => x.ItemPlace == loot.ItemPlace))
            {
                var item = items.Find(x => x.ItemPlace == loot.ItemPlace);
                return item.ToString();
            }
            else return "Nothing equipped.";
        }

        private DAL.Item convertToDbItem(CharacterClassLibrary.Item item)
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
            return DALItem;
        }
    }
}

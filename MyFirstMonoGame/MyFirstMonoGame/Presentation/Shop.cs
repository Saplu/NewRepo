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
    public class Shop
    {
        Texture2D backGround;
        ShopClassLibrary.Shop shop;
        Label offerLabel, exceptionLabel, moneyLabel, currentLabel;
        Button okButton, menuButton, player1Button, player2Button, player3Button, player4Button,
            helmetButton, chestButton, handButton, legButton, feetButton;
        List<CharacterClassLibrary.Player> players;
        List<Button> allButtons, playerButtons, placeButtons;

        public Shop(List<CharacterClassLibrary.Player> players, DAL.DAO dao, Texture2D backGround,
            Texture2D buttonTexture, SpriteFont font)
        {
            this.backGround = backGround;
            this.players = players;
            shop = new ShopClassLibrary.Shop(players, dao);
            offerLabel = new Label("offerLabel", font, "", 440, 40, 300, 300);
            exceptionLabel = new Label("exceptionLabel", font, "", 320, 240, 400, 30);
            moneyLabel = new Label("moneyLabel", font, "Your money: " + shop.Dao.Party.Money,
                20, 350, 150, 30);
            currentLabel = new Label("currentLabel", font, "", 200, 40, 300, 300);
            okButton = new Button(buttonTexture, 700, 340, "Go for it!", 80, 30);
            menuButton = new Button(buttonTexture, 700, 400, "Menu", 80, 30);
            player1Button = new Button(buttonTexture, 20, 50, players[0].Name, 100, 30);
            player2Button = new Button(buttonTexture, 20, 130, players[1].Name, 100, 30);
            player3Button = new Button(buttonTexture, 20, 210, players[2].Name, 100, 30);
            player4Button = new Button(buttonTexture, 20, 290, players[3].Name, 100, 30);
            helmetButton = new Button(buttonTexture, 20, 400, "Helmet", 60, 30);
            chestButton = new Button(buttonTexture, 100, 400, "Chest", 60, 30);
            handButton = new Button(buttonTexture, 180, 400, "Hands", 60, 30);
            legButton = new Button(buttonTexture, 260, 400, "Legs", 60, 30);
            feetButton = new Button(buttonTexture, 340, 400, "Feet", 60, 30);
            allButtons = new List<Button>() { okButton, menuButton, player1Button, player2Button,
            player3Button, player4Button, helmetButton, chestButton, handButton, legButton, feetButton};
            playerButtons = new List<Button>() { player1Button, player2Button, player3Button, player4Button };
            placeButtons = new List<Button>() { helmetButton, chestButton, handButton, legButton, feetButton };
        }

        public void Draw(SpriteBatch sprite, SpriteFont font)
        {
            sprite.Draw(backGround, new Rectangle(0, 0, 800, 480), Color.White);
            offerLabel.Draw(sprite, font);
            exceptionLabel.Draw(sprite, font);
            moneyLabel.Draw(sprite, font);
            currentLabel.Draw(sprite, font);
            foreach (var button in allButtons)
                button.Draw(sprite, font);
        }

        public void UpdateButtons(MouseState currentState)
        {
            foreach (var button in allButtons)
                button.UpdateMouseState(currentState);
        }

        public string CheckButtons()
        {
            try
            {
                foreach (var button in playerButtons)
                {
                    if (button.ButtonClicked())
                    {
                        exceptionLabel.Text = "";
                        shop.SelectedPlayer = players.Find(x => x.Name == button.Text);
                    }
                }
                foreach (var button in placeButtons)
                {
                    if (button.ButtonClicked())
                    {
                        exceptionLabel.Text = "";
                        shop.GenerateOffer(button.Text);
                        offerLabel.Text = "Offer:\r\n" + shop.Offer.ToString(1);
                        currentLabel.Text = "Currently wearing:\r\n" + shop.SelectedPlayer.Items[placeButtons.IndexOf(button) + 2];
                    }
                }
                if (okButton.ButtonClicked())
                {
                    var party = shop.PurcasheItem();
                    var converter = new PlayerConverter();
                    var dalParty = converter.GameToDAO(party);
                    dalParty.Money = shop.Money;
                    shop.SaveChanges(dalParty);
                    moneyLabel.Text = dalParty.Money.ToString();
                    return "reload";
                }
                if (menuButton.ButtonClicked())
                    return "MainMenu";
                return "Shop";
            }
            catch(Exception ex)
            {
                exceptionLabel.Text = ex.Message;
                return "Shop";
            }
        }
    }
}

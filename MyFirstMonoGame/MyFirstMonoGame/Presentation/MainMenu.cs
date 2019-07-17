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
    public class MainMenu
    {
        Button PlayButton, NewPlayerButton, ShopButton, ExitButton, ChoosePartyButton;
        Texture2D BackGround;
        public List<Button> MenuButtons;

        public MainMenu(Texture2D buttonTexture, Texture2D backGroundTexture, SpriteFont font)
        {
            PlayButton = new Button(buttonTexture, 275, 100, "Enter World", 250, 40);
            NewPlayerButton = new Button(buttonTexture, 275, 160, "New Character", 250, 40);
            ShopButton = new Button(buttonTexture, 275, 220, "Shop", 250, 40);
            ExitButton = new Button(buttonTexture, 275, 340, "Exit", 250, 40);
            ChoosePartyButton = new Button(buttonTexture, 275, 280, "Choose Party", 250, 40);
            BackGround = backGroundTexture;
            MenuButtons = new List<Button>() { PlayButton, NewPlayerButton, ShopButton, ExitButton, ChoosePartyButton };
        }

        public void Draw(SpriteBatch sprite, SpriteFont font)
        {
            sprite.Draw(BackGround, new Rectangle(0, 0, 800, 480), Color.White);
            PlayButton.Draw(sprite, font);
            NewPlayerButton.Draw(sprite, font);
            ShopButton.Draw(sprite, font);
            ChoosePartyButton.Draw(sprite, font);
            ExitButton.Draw(sprite, font);
        }

        public string CheckButtons()
        {
            if (PlayButton.ButtonClicked())
                return "Adventure";
            if (ExitButton.ButtonClicked())
                return "Exit";
            if (ShopButton.ButtonClicked())
                return "Shop";
            if (ChoosePartyButton.ButtonClicked())
                return "PartySelect";
            if (NewPlayerButton.ButtonClicked())
                return "PartySelect";
            else return "MainMenu";
        }

        public void UpdateButtons(MouseState currentState)
        {
            foreach (var button in MenuButtons)
                button.UpdateMouseState(currentState);
        }
    }
}

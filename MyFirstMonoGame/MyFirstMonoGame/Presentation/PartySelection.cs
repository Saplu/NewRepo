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
    public class PartySelection
    {
        Texture2D backGround;
        Label p1Label, p2Label, p3Label, p4Label;
        Button p1Button, p2Button, p3Button, p4Button, okButton, menuButton;
        List<Button> allButtons;
        DAL.DAO dao;
        int selection;

        public PartySelection(Texture2D backGround, Texture2D buttonTexture, SpriteFont font)
        {
            selection = 1;
            this.backGround = backGround;
            dao = new DAL.DAO(selection);
            dao.Read();
            p1Label = new Label("p1", font, dao.Players[0].ToMenuString(), 50, 200, 300, 50);
            p2Label = new Label("p2", font, dao.Players[1].ToMenuString(), 50, 250, 300, 50);
            p3Label = new Label("p3", font, dao.Players[2].ToMenuString(), 50, 300, 300, 50);
            p4Label = new Label("p4", font, dao.Players[3].ToMenuString(), 50, 350, 300, 50);
            p1Button = new Button(buttonTexture, 50, 100, "Party 1", 120, 40);
            p2Button = new Button(buttonTexture, 200, 100, "Party 2", 120, 40);
            p3Button = new Button(buttonTexture, 350, 100, "Party 3", 120, 40);
            p4Button = new Button(buttonTexture, 500, 100, "Party 4", 120, 40);
            okButton = new Button(buttonTexture, 650, 150, "Confirm", 120, 40);
            menuButton = new Button(buttonTexture, 650, 300, "Menu", 120, 40);
            allButtons = new List<Button>() { p1Button, p2Button, p3Button, p4Button, okButton, menuButton };
        }

        public void Draw(SpriteBatch sprite, SpriteFont font)
        {
            sprite.Draw(backGround, new Rectangle(0, 0, 800, 480), Color.White);
            p1Label.Draw(sprite, font);
            p2Label.Draw(sprite, font);
            p3Label.Draw(sprite, font);
            p4Label.Draw(sprite, font);
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
            if(p1Button.ButtonClicked())
            {
                selection = 1;
                newData();
            }
            if (p2Button.ButtonClicked())
            {
                selection = 2;
                newData();
            }
            if(p3Button.ButtonClicked())
            {
                selection = 3;
                newData();
            }
            if(p4Button.ButtonClicked())
            {
                selection = 4;
                newData();
            }
            if(okButton.ButtonClicked())
            {
                return selection.ToString();
            }
            if(menuButton.ButtonClicked())
            {
                return "MainMenu";
            }
            return "PartySelect";
        }

        private void newData()
        {
            dao = new DAL.DAO(selection);
            dao.Read();
            p1Label.Update(dao.Players[0].ToMenuString());
            p2Label.Update(dao.Players[1].ToMenuString());
            p3Label.Update(dao.Players[2].ToMenuString());
            p4Label.Update(dao.Players[3].ToMenuString());
            
        }
    }
}

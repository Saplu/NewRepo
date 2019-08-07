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
    public class NewParty
    {
        Texture2D backGround;
        Button okButton, cancelButton, finishButton, menuButton, warriorButton, mageButton, bloodButton,
            protectorButton, fairyButton, shamanButton, rogueButton, templarButton, p1Button, p2Button, p3Button, p4Button;
        Label partyLabel, infoLabel;
        TextBox nameBox;
        List<Button> allButtons;
        DAL.DAO dao;
        int selectedParty, selectedClass;
        CharacterClassLibrary.Party party;

        public NewParty(Texture2D backGround, Texture2D buttonTexture, SpriteFont font)
        {
            selectedParty = 1;
            selectedClass = 0;
            dao = new DAL.DAO(selectedParty);
            dao.Read();
            this.backGround = backGround;
            protectorButton = new Button(buttonTexture, 30, 30, "Protector", 100, 40);
            templarButton = new Button(buttonTexture, 30, 80, "Templar", 100, 40);
            warriorButton = new Button(buttonTexture, 30, 130, "Warrior", 100, 40);
            mageButton = new Button(buttonTexture, 30, 180, "Mage", 100, 40);
            shamanButton = new Button(buttonTexture, 30, 230, "Shaman", 100, 40);
            rogueButton = new Button(buttonTexture, 30, 280, "Rogue", 100, 40);
            bloodButton = new Button(buttonTexture, 30, 330, "Blood Priest", 100, 40);
            fairyButton = new Button(buttonTexture, 30, 380, "Fairy", 100, 40);
            p1Button = new Button(buttonTexture, 300, 15, "Party 1", 100, 40);
            p2Button = new Button(buttonTexture, 420, 15, "Party 2", 100, 40);
            p3Button = new Button(buttonTexture, 540, 15, "Party 3", 100, 40);
            p4Button = new Button(buttonTexture, 660, 15, "Party 4", 100, 40);
            cancelButton = new Button(buttonTexture, 680, 250, "Clear", 100, 40);
            okButton = new Button(buttonTexture, 680, 300, "Next", 100, 40);
            finishButton = new Button(buttonTexture, 680, 350, "Done", 100, 40);
            menuButton = new Button(buttonTexture, 680, 400, "Main Menu", 100, 40);

            allButtons = new List<Button>() { protectorButton, templarButton, warriorButton, mageButton, shamanButton, rogueButton,
            bloodButton, fairyButton, p1Button, p2Button, p3Button, p4Button, cancelButton, okButton, finishButton, menuButton};

            partyLabel = new Label("party", font, "", 200, 130, 400, 200);
            infoLabel = new Label("info", font, "Enter name: ", 200, 400, 100, 50);
            nameBox = new TextBox(font, 290, 400, 150, 50);

            party = new CharacterClassLibrary.Party();
        }

        public void Draw(SpriteBatch sprite, SpriteFont font)
        {
            sprite.Draw(backGround, new Rectangle(0, 0, 800, 480), Color.White);
            foreach (var button in allButtons)
                button.Draw(sprite, font);
            partyLabel.Draw(sprite, font, Color.White);
            infoLabel.Draw(sprite, font, Color.White);
            nameBox.Draw(sprite, font, Color.White);
        }

        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            nameBox.Update(mouse, keyboard);
            partyLabel.Text = getPartyInfo();
        }

        public void UpdateButtons(MouseState currentState)
        {
            foreach (var button in allButtons)
                button.UpdateMouseState(currentState);
        }

        public string CheckButtons()
        {
            if (menuButton.ButtonClicked())
                return "MainMenu";
            nameBox.Clicked();
            classButtons();
            partyButtons();
            if (okButton.ButtonClicked())
                generatePlayer();
            if (cancelButton.ButtonClicked())
                removePlayer();
            if (finishButton.ButtonClicked())
                createParty();
            return "NewParty";
        }

        private void classButtons()
        {
            if (warriorButton.ButtonClicked())
                selectedClass = 0;
            else if (mageButton.ButtonClicked())
                selectedClass = 1;
            else if (bloodButton.ButtonClicked())
                selectedClass = 2;
            else if (protectorButton.ButtonClicked())
                selectedClass = 3;
            else if (fairyButton.ButtonClicked())
                selectedClass = 4;
            else if (shamanButton.ButtonClicked())
                selectedClass = 5;
            else if (rogueButton.ButtonClicked())
                selectedClass = 6;
            else if (templarButton.ButtonClicked())
                selectedClass = 7;
        }

        private void partyButtons()
        {
            if (p1Button.ButtonClicked())
                selectedParty = 1;
            else if (p2Button.ButtonClicked())
                selectedParty = 2;
            else if (p3Button.ButtonClicked())
                selectedParty = 3;
            else if (p4Button.ButtonClicked())
                selectedParty = 4;
        }

        private string getPartyInfo()
        {
            var value = "Selected party: " + selectedParty.ToString() + "\r\n";

            foreach (var player in party.Players)
                value += player.Name + "\r\n" + player.ClassName.ToString() + "\r\n";
            var current = Enum.Parse(typeof(CharacterClassLibrary.Enums.ClassName), selectedClass.ToString());
            if (party.Players.Count < 4)
                value += "\r\n" + current;
            return value;
        }

        private void generatePlayer()
        {
            try
            {
                infoLabel.Text = "Enter Name: ";
                if (party.Players.Count < 4)
                {
                    var player = CharacterClassLibrary.Player.Create((CharacterClassLibrary.Enums.ClassName)Enum.Parse(typeof
                        (CharacterClassLibrary.Enums.ClassName), selectedClass.ToString()));
                    player.Name = getPlayerName();
                    party.Players.Add(player);
                    nameBox.Text = "";
                }
                else throw new Exception("Party full");
            }
            catch(Exception ex)
            {
                infoLabel.Text = ex.Message;
            }
        }

        private string getPlayerName()
        {
            if (nameBox.Text.Trim().Length != 0)
                return nameBox.Text;
            else throw new Exception("Invalid name");
        }

        private void removePlayer()
        {
            if (party.Players.Count > 0)
                party.Players.RemoveAt(party.Players.Count - 1);
        }

        private void createParty()
        {
            try
            {
                if (party.Players.Count == 4)
                {
                    dao.Number = selectedParty;
                    var converter = new PlayerConverter();
                    var dalParty = converter.GameToDAO(party);
                    dao.Update(dalParty);
                }
                else throw new Exception("Party not yet full");
            }
            catch(Exception ex)
            {
                infoLabel.Text = ex.Message;
            }
        }
    }
}

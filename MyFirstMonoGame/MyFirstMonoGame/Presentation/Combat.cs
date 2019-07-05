using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MissionClassLibrary;

namespace MyFirstMonoGame.Presentation
{
    public class Combat
    {
        private Mission mission;
        private MissionStatus missionStatus;
        private List<Button> enemyButtons;
        private List<Button> playerButtons;
        private Texture2D backGround, available, unavailable, selected;
        private Button skill1Button, skill2Button, skill3Button, skill4Button, nextTurnButton;
        private Label enemy1Label, enemy2Label, enemy3Label, enemy4Label, 
            player1Label, player2Label, player3Label, player4Label, turnLabel, toolTipLabel;
        private List<Button> allButtons, skillUsableButtons, playerUsableButtons, skillButtons;

        public Combat(Mission mission, Texture2D backGround, List<Texture2D> characterTextures, SpriteFont font, Texture2D skillButton,
            Texture2D available, Texture2D unavailable, Texture2D selected)
        {
            this.mission = mission;
            missionStatus = new MissionStatus();
            this.backGround = backGround;
            this.available = available;
            this.unavailable = unavailable;
            this.selected = selected;
            enemyButtons = new List<Button>();
            playerButtons = new List<Button>();
            playerUsableButtons = new List<Button>();

            foreach (var enemy in mission.Enemies)
            {
                var textureID = enemy.setPic();
                var texture = characterTextures.Find(x => x.Name == textureID);
                var buttonX = 150 + ((enemy.Position - 5) * 150);

                var button = new Button(texture, buttonX, 25, "", 75, 75);
                enemyButtons.Add(button);
            }
            foreach (var player in mission.Players)
            {
                var textureID = player.setPic();
                var texture = characterTextures.Find(x => x.Name == textureID);
                var buttonX = 150 + ((player.Position - 1) * 150);

                var button = new Button(texture, buttonX, 200, "", 75, 75);
                playerButtons.Add(button);
                var info = new Button(available, buttonX, 275, "", 75, 2);
                playerUsableButtons.Add(info);
            }

            enemy1Label = new Label("enemy1Label", font, mission.Enemies[0].ToString(), 150, 100, 150, 100);
            if (mission.Enemies.Count > 1)enemy2Label = new Label("enemy2Label", font, mission.Enemies[1].ToString(), 300, 100, 150, 100);
            if (mission.Enemies.Count > 2)enemy3Label = new Label("enemy3Label", font, mission.Enemies[2].ToString(), 450, 100, 150, 100);
            if (mission.Enemies.Count >3)enemy4Label = new Label("enemy4Label", font, mission.Enemies[3].ToString(), 600, 100, 150, 100);
            player1Label = new Label("player1Label", font, mission.Players[0].ToString(), 150, 277, 150, 100);
            player2Label = new Label("player2Label", font, mission.Players[1].ToString(), 300, 277, 150, 100);
            player3Label = new Label("player3Label", font, mission.Players[2].ToString(), 450, 277, 150, 100);
            player4Label = new Label("player4Label", font, mission.Players[3].ToString(), 600, 277, 150, 100);
            turnLabel = new Label("turnLabel", font, "Turn: " + missionStatus.Turn.ToString(), 725, 230, 50, 20);
            toolTipLabel = new Label("toolTip", font, "", 5, 240, 130, 240);

            skill1Button = new Button(skillButton, 120, 420, "", 130, 40);
            skill2Button = new Button(skillButton, 270, 420, "", 130, 40);
            skill3Button = new Button(skillButton, 420, 420, "", 130, 40);
            skill4Button = new Button(skillButton, 570, 420, "", 130, 40);
            var Usable1 = new Button(available, 130, 460, "", 110, 2);
            var Usable2 = new Button(available, 280, 460, "", 110, 2);
            var Usable3 = new Button(available, 430, 460, "", 110, 2);
            var Usable4 = new Button(available, 580, 460, "", 110, 2);
            nextTurnButton = new Button(skillButton, 710, 370, "End Turn", 80, 40);

            skillButtons = new List<Button>() { skill1Button, skill2Button, skill3Button, skill4Button };
            skillUsableButtons = new List<Button>() { Usable1, Usable2, Usable3, Usable4 };
            allButtons = new List<Button>();
            allButtons.Add(nextTurnButton);
            allButtons.AddRange(skillButtons);
            allButtons.AddRange(enemyButtons);
            allButtons.AddRange(playerButtons);
        }

        public Combat()
        {

        }

        public void Draw(SpriteBatch sprite, SpriteFont font)
        {
            sprite.Draw(backGround, new Rectangle(0, 0, 800, 480), Color.White);
            foreach (var button in enemyButtons)
                button.Draw(sprite, font);
            foreach (var button in playerButtons)
                button.Draw(sprite, font);
            enemy1Label.Draw(sprite, font);
            if (mission.Enemies.Count > 1) enemy2Label.Draw(sprite, font);
            if (mission.Enemies.Count > 2) enemy3Label.Draw(sprite, font);
            if (mission.Enemies.Count > 3) enemy4Label.Draw(sprite, font);
            player1Label.Draw(sprite, font);
            player2Label.Draw(sprite, font);
            player3Label.Draw(sprite, font);
            player4Label.Draw(sprite, font);
            skill1Button.Draw(sprite, font);
            skill2Button.Draw(sprite, font);
            skill3Button.Draw(sprite, font);
            skill4Button.Draw(sprite, font);
            nextTurnButton.Draw(sprite, font);
            turnLabel.Draw(sprite, font);
            foreach (var button in playerUsableButtons)
                button.Draw(sprite, font);
            foreach (var button in skillUsableButtons)
                button.Draw(sprite, font);
            toolTipLabel.Draw(sprite, font);
        }

        public void Update()
        {
            player1Label.Text = mission.Players[0].ToString();
            player2Label.Text = mission.Players[1].ToString();
            player3Label.Text = mission.Players[2].ToString();
            player4Label.Text = mission.Players[3].ToString();
            enemy1Label.Text = mission.Enemies[0].ToString();
            if (mission.Enemies.Count > 1) enemy2Label.Text = mission.Enemies[1].ToString();
            if (mission.Enemies.Count > 2) enemy3Label.Text = mission.Enemies[2].ToString();
            if (mission.Enemies.Count > 3) enemy4Label.Text = mission.Enemies[3].ToString();
            turnLabel.Text = missionStatus.Turn.ToString();
            foreach(var player in mission.Players)
            {
                if (missionStatus.ActionDone.Contains(mission.Players.IndexOf(player) + 1))
                    playerUsableButtons[mission.Players.IndexOf(player)].Texture = unavailable;
                else if (missionStatus.SelectedPlayerPosition - 1 == mission.Players.IndexOf(player))
                    playerUsableButtons[mission.Players.IndexOf(player)].Texture = selected;
                else playerUsableButtons[mission.Players.IndexOf(player)].Texture = available;
            }
            foreach(var button in skillUsableButtons)
            {
                if (missionStatus.Cdarr[skillUsableButtons.IndexOf(button)] > 0)
                    button.Texture = unavailable;
                else if (missionStatus.SkillID == skillButtons[skillUsableButtons.IndexOf(button)].Text)
                    button.Texture = selected;
                else button.Texture = available;
            }
            foreach (var button in skillButtons)
            {
                if (button.MouseOver() && missionStatus.SelectedPlayerPosition != 0)
                {
                    toolTipLabel.Update(mission.Players[missionStatus.SelectedPlayerPosition - 1].AbilityInfo()[skillButtons.IndexOf(button)]);
                }
            }
        }

        public string CheckButtons()
        {
            try
            {
                if (missionStatus.SelectedPlayerPosition == 0)
                    checkPlayerButtons();
                else if (missionStatus.SkillID == "")
                    checkSkillButtons();
                else checkCharacterButtons(missionStatus.TargetSide);
                if (nextTurnButton.ButtonClicked())
                {
                    missionStatus.EndTurn();
                    mission.EndTurn();
                    if (mission.CheckLoss())
                        return "Lose";
                    else if (mission.CheckWin())
                    {
                        mission.EndOfMissionReset();
                        return "Victory";
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return "Combat";
        }

        public void UpdateButtons(MouseState currentState)
        {
            foreach (var button in allButtons)
                button.UpdateMouseState(currentState);
        }

        private void checkPlayerButtons()
        {
            foreach(var button in playerButtons)
            {
                if (button.ButtonClicked() && !missionStatus.ActionDone.Contains(playerButtons.IndexOf(button) + 1))
                {
                    missionStatus.SkillID = "";
                    var index = playerButtons.IndexOf(button);
                    missionStatus.SelectedPlayerPosition = index + 1;
                    skill1Button.Text = mission.Players[index].Ability1()[0];
                    skill2Button.Text = mission.Players[index].Ability2()[0];
                    skill3Button.Text = mission.Players[index].Ability3()[0];
                    skill4Button.Text = mission.Players[index].Ability4()[0];
                }
            }
        }

        private void checkCharacterButtons(int side)
        {
            if (side == 1)
            {
                foreach (var button in playerButtons)
                {
                    if (button.ButtonClicked())
                    {
                        var position = playerButtons.IndexOf(button) +1;
                        mission.PlayerHeal(position, missionStatus.SelectedPlayerPosition, missionStatus.SkillID);
                        mission.SetStatuses(missionStatus.SelectedPlayerPosition, missionStatus.SkillID, position);
                        missionStatus.ActionDone.Add(missionStatus.SelectedPlayerPosition);
                        missionStatus.SkillID = "";
                        missionStatus.SelectedPlayerPosition = 0;
                    }
                }
            }
            if (side == 2)
            {
                foreach (var button in enemyButtons)
                {
                    if (button.ButtonClicked())
                    {
                        var position = enemyButtons.IndexOf(button) + 5;
                        mission.EnemyDefend(position, missionStatus.SelectedPlayerPosition, missionStatus.SkillID);
                        mission.SetStatuses(missionStatus.SelectedPlayerPosition, missionStatus.SkillID, position);
                        missionStatus.ActionDone.Add(missionStatus.SelectedPlayerPosition);
                        missionStatus.SkillID = "";
                        missionStatus.SelectedPlayerPosition = 0;
                    }
                }
                checkPlayerButtons();
            }
            checkSkillButtons();
        }

        private void checkSkillButtons()
        {
            missionStatus.Cdarr = mission.CheckCooldowns(missionStatus.SelectedPlayerPosition - 1);
            if (skill1Button.ButtonClicked() && missionStatus.Cdarr[0] < 1)
                missionStatus.SetSkillID(skill1Button.Text);
            else if (skill2Button.ButtonClicked() && missionStatus.Cdarr[1] < 1)
                missionStatus.SetSkillID(skill2Button.Text);
            else if (skill3Button.ButtonClicked() && missionStatus.Cdarr[2] < 1)
                missionStatus.SetSkillID(skill3Button.Text);
            else if (skill4Button.ButtonClicked() && missionStatus.Cdarr[3] < 1)
                missionStatus.SetSkillID(skill4Button.Text);
            else checkPlayerButtons();
        }
    }
}

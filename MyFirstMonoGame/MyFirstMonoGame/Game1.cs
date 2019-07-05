using CharacterClassLibrary.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MyFirstMonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game //Tämä on se oikea kansio!
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Map map;
        Hero hero;
        Presentation.MainMenu mainMenu;
        Presentation.Combat combat;
        bool Main, Adventure, Combat, Victory;
        MouseState previousState, currentState;
        int x, y;
        List<Texture2D> characterTextures;
        MissionClassLibrary.Mission activeMission;
        List<CharacterClassLibrary.Player> players;
        Presentation.VictoryView victory;
        Texture2D combatBackGround, skillButtonTexture, victoryBackGround, buttonTexture, red, blue, green;

        DAL.DAO dao;

        public Map Map { get => map; set => map = value; }
        public Hero Hero { get => hero; set => hero = value; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            Main = true;
            dao = new DAL.DAO(1);
            dao.Read();
            var playerConverter = new PlayerConverter();
            players = playerConverter.DAOToGame(dao.Players);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var heroTextureAtlas = Content.Load<Texture2D>("hero");
            var mapTextureAtlas = Content.Load<Texture2D>("seinät");
            font = Content.Load<SpriteFont>("Font");
            hero = new Hero(heroTextureAtlas, 4, 4);
            buttonTexture = Content.Load<Texture2D>("Nappi");
            var menuBackGround = Content.Load<Texture2D>("Tausta");
            mainMenu = new Presentation.MainMenu(buttonTexture, menuBackGround, font);
            skillButtonTexture = Content.Load<Texture2D>("skillbutton");

            var bloodPriestTexture = Content.Load<Texture2D>("BloodPriest");
            var goblinTexture = Content.Load<Texture2D>("Goblin");
            var keeperTexture = Content.Load<Texture2D>("keeper");
            var fairyTexture = Content.Load<Texture2D>("keijju");
            var kingTexture = Content.Load<Texture2D>("kunkku");
            var medicTexture = Content.Load<Texture2D>("medic");
            var pirateTexture = Content.Load<Texture2D>("merkkari");
            var necroTexture = Content.Load<Texture2D>("necro");
            var warriorTexture = Content.Load<Texture2D>("ninja");
            var templarTexture = Content.Load<Texture2D>("paladin");
            var rabbitTexture = Content.Load<Texture2D>("Pasi");
            var rogueTexture = Content.Load<Texture2D>("rogue");
            var shamanTexture = Content.Load<Texture2D>("shamaani");
            var protectorTexture = Content.Load<Texture2D>("Tankki");
            var mageTexture = Content.Load<Texture2D>("welho");
            var enemyTexture = Content.Load<Texture2D>("enemy");
            green = Content.Load<Texture2D>("greenButton");
            blue = Content.Load<Texture2D>("blueButton");
            red = Content.Load<Texture2D>("redButton");

            combatBackGround = Content.Load<Texture2D>("CombatBackGround");
            victoryBackGround = Content.Load<Texture2D>("voittotausta");


            characterTextures = new List<Texture2D>() { bloodPriestTexture, goblinTexture, keeperTexture, fairyTexture, kingTexture,
                medicTexture, pirateTexture, necroTexture, warriorTexture, templarTexture,
                rabbitTexture, rogueTexture, shamanTexture, protectorTexture, mageTexture};

            map = new Maps.Training(mapTextureAtlas, 5, 7, enemyTexture);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            currentState = Mouse.GetState();

            if (Main == true)
            {
                mainMenu.UpdateButtons(currentState);
                string Redirect = mainMenu.CheckButtons();
                manageActiveObjects(Redirect);
            }

            if (Adventure == true)
            {
                hero.Update(gameTime, graphics);
                hero.CheckForCollision(map.Boxes, gameTime, graphics);
                string Redirect = hero.CheckForCombat(map.CombatBoxes);
                manageActiveObjects(Redirect);
                if (Redirect == "Combat")
                {
                    newCombat();
                }
            }
            if (Combat == true)
            {
                combat.UpdateButtons(currentState);
                string Redirect = combat.CheckButtons();
                combat.Update();
                manageActiveObjects(Redirect);
            }
            if (Victory == true)
            {
                victory.UpdateButtons(currentState);
                string Redirect = victory.CheckButtons();
                manageActiveObjects(Redirect);
                if (Redirect == "Adventure")
                {
                    savePlayers();
                }
            }
            previousState = currentState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (Adventure == true)
            {
                map.Draw(spriteBatch);
                hero.Draw(spriteBatch);
            }
            if (Main == true)
            {
                mainMenu.Draw(spriteBatch, font);
            }
            if (Combat == true)
            {
                combat.Draw(spriteBatch, font);
            }
            if (Victory == true)
            {
                victory.Draw(spriteBatch, font);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void manageActiveObjects(string key)
        {
            switch (key)
            {
                case "MainMenu": Main = true; Adventure = false; Combat = false; Victory = false; break;
                case "Adventure": Main = false; Adventure = true; Combat = false; Victory = false; break;
                case "Combat": Main = false; Adventure = false; Combat = true; Victory = false; break;
                case "Victory": Main = false; Adventure = false; Combat = false; Victory = true; break;
                case "Exit": Exit(); break;
                default: Main = true; Adventure = false; Combat = false; Victory = false; break;
            }
        }

        private void newCombat()
        {
            var randomMission = new MissionClassLibrary.RandomMissionGenerator();
            activeMission = randomMission.CreateMission(map.Level, map.MapDifficulty, players);
            combat = new Presentation.Combat(activeMission, combatBackGround, characterTextures, font, skillButtonTexture, green, red, blue);
            var mission = new MissionClassLibrary.SuccessfulMission(activeMission);
            victory = new Presentation.VictoryView(victoryBackGround, mission, buttonTexture, font, hero, map);
        }

        private void savePlayers()
        {
            var converter = new PlayerConverter();
            var party = converter.GameToDAO(activeMission.Players);
            dao.Update(party);
            dao.Read();
            players = converter.DAOToGame(dao.Players);
        }
    }
}

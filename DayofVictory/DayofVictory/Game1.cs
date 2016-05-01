using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

using DayofVictory.AI;

namespace DayofVictory
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public const int GAME_SIZE_X = 1000, GAME_SIZE_Y = 720;
        public const int MAX_RECENTS = 10; //How many moves do we keep track of?

        SpriteBatch spriteBatch;
        private ScreenManager.ScreenManager screenManager;
                             
        //I really wish we didn't do it this way, for what it's worth.
        public static Ship playerShip;
        public static AIShip enemyShip;
        public static AIShipCalculator aiCalculator;

        private static bool playersTurn;

        private Vicky vicky;
        private Enemy enemy;
        private Texture2D gameBackground;
        private Texture2D vickyHappy; // Vicky being fine img
        private Texture2D vickyHurt; // Vicky hurt img
        private Texture2D vickyShoot; // Vicky shooting icon
        private Texture2D vickyRepair; // Vicky repairing icon
        private Texture2D vickyBail; // Vicky baling icon
        private Texture2D enemyShipImg; // enemy ship img
        private Texture2D boom; // boom effect
        private Texture2D explosionCloud; // cloud effect

        private Watch watch;

        public static List<string> recentMoves;

        public Game1()
        {
            Globals.Globals.graphics = new GraphicsDeviceManager(this);
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
            playersTurn = true;
            vicky = new Vicky();
            enemy = new Enemy();

            watch = new Watch();

            //For what it's worth, I also object to these namespaces.
            Globals.Globals.gameSize = new Vector2(GAME_SIZE_X, GAME_SIZE_Y);
            Globals.Globals.graphics.PreferredBackBufferWidth = (int)Globals.Globals.gameSize.X;
            Globals.Globals.graphics.PreferredBackBufferHeight = (int)Globals.Globals.gameSize.Y;
            Globals.Globals.graphics.IsFullScreen = true;
            Globals.Globals.graphics.ApplyChanges();
            vicky.setUnderAttack(watch);
            enemy.setUnderAttack(watch);

            recentMoves = new List<string>();

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
            Globals.Globals.spriteBatch = spriteBatch;
            Globals.Globals.content = Content; //Give global handlers to things we need

            playerShip = new Ship(this, null);
            enemyShip = new AIShip(this, null, playerShip);

            gameBackground = Content.Load<Texture2D>("images/background");
            vickyHappy = Content.Load<Texture2D>("images/VickyHappy");
            vickyHurt = Content.Load<Texture2D>("images/VickyHurt");
            vickyShoot = Content.Load<Texture2D>("images/VickyShooting");
            vickyRepair = Content.Load<Texture2D>("images/VickyRepair");
            vickyBail = Content.Load<Texture2D>("images/VickyBailing");
            enemyShipImg = Content.Load<Texture2D>("images/EnemyShip");
            boom = Content.Load<Texture2D>("images/boom");
            explosionCloud = Content.Load<Texture2D>("images/explosionCloud");

            // TODO: call all resource.load() methods
           Globals.Resources.Fonts.load();
           Globals.Resources.Textures.load();

           screenManager = new ScreenManager.ScreenManager();
            //Add title screen here if we want it
           ScreenManager.ScreenManager.addScreen(new ScreenManager.Screens.GameScreen());
           ScreenManager.ScreenManager.addScreen(new ScreenManager.Screens.HUDScreen());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
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
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            watch.updTime(gameTime);

            Globals.Input.Update();
            screenManager.Update(delta);

            vicky.hurtIconCheck(watch);
            enemy.hurtIconCheck(watch);

            if (!playersTurn)
            {
                vicky.resetStates();

                playerShip.TakeOnWater();

                if (playerShip.WaterTaken() >= Ship.MAX_WATER)
                {
                    //TODO Transition to game over
                }

                enemyShip.DoMove();
                enemyShip.TakeOnWater();

                if (enemyShip.WaterTaken() >= Ship.MAX_WATER)
                {
                    //TODO Transition to game over
                }

                playersTurn = true;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
                spriteBatch.Draw(gameBackground, new Rectangle(0, 0, 1000, 720), Color.White);
                spriteBatch.Draw(enemyShipImg, new Rectangle(300, 290, 329, 177), Color.White);
                
                if (enemy.isUnderAttack())
                {
                    spriteBatch.Draw(boom, new Rectangle(370, 400, 80, 50), Color.White);
                    spriteBatch.Draw(explosionCloud, new Rectangle(500, 310, 80, 90), Color.White);
                }
                
                if (vicky.isUnderAttack()) spriteBatch.Draw(vickyHurt, new Rectangle(350, 600, 100, 100), Color.White);
                else if (vicky.isShooting()) spriteBatch.Draw(vickyShoot, new Rectangle(350, 600, 100, 100), Color.White);
                else if (vicky.isRepairing()) spriteBatch.Draw(vickyRepair, new Rectangle(350, 600, 100, 100), Color.White);
                else if (vicky.isBailing()) spriteBatch.Draw(vickyBail, new Rectangle(350, 600, 100, 100), Color.White);
                else spriteBatch.Draw(vickyHappy, new Rectangle(350, 600, 100, 100), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
            screenManager.Draw();
        }

        //This is probably not where this should be living.
        public static bool IsPlayersTurn()
        {
            return playersTurn;
        }

        public static void setPlayersTurn(bool turn)
        {
            playersTurn = turn;
        }

        public static void togglePlayersTurn()
        {
            playersTurn = !playersTurn;
        }

        public static void TrimRecentsList()
        {
            if (recentMoves.Count > MAX_RECENTS)
            {
                while(recentMoves.Count > MAX_RECENTS)
                {
                    recentMoves.RemoveAt(0);
                }
            }
        }
    }
}

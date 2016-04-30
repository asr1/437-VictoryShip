
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using DayofVictory.AI;

namespace DayofVictory
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public const int GAME_SIZE_X = 800, GAME_SIZE_Y = 400;
        SpriteBatch spriteBatch;
        private ScreenManager.ScreenManager screenManager;
                             
        //I really wish we didn't do it this way, for what it's worth.
        public static Ship playerShip;
        public static AIShip enemyShip;
        public static AIShipCalculator aiCalculator;

        private static bool playersTurn;

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

            //For what it's worth, I also object to these namespaces.
            Globals.Globals.gameSize = new Vector2(GAME_SIZE_X, GAME_SIZE_Y);
            Globals.Globals.graphics.PreferredBackBufferWidth = (int)Globals.Globals.gameSize.X;
            Globals.Globals.graphics.PreferredBackBufferHeight = (int)Globals.Globals.gameSize.Y;
            Globals.Globals.graphics.ApplyChanges();

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
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // TODO: Add your update logic here            
            Globals.Input.Update();
            screenManager.Update(delta);

            if (!playersTurn)
            {
                playerShip.TakeOnWater();
                //TODO Check for game over
                enemyShip.DoMove();
                enemyShip.TakeOnWater();
                //TODO Check for game over
            }

            base.Update(gameTime);
            screenManager.Update(delta);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

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
    }
}

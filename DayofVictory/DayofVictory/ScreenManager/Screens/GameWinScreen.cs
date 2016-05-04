using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayofVictory.ScreenManager.Screens
{
    class GameWinScreen : BaseScreen
    {
        private Texture2D gameWinScreen;

        public GameWinScreen()
        {
            gameWinScreen = Globals.Globals.content.Load<Texture2D>("screens/GameWinVictory");
            name = "GameWinScreen";
            MediaPlayer.Stop();
            state = ScreenState.Active;
        }

        public override void Update(float delta)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ScreenManager.unloadScreen(name);
                ScreenManager.addScreen(new GameScreen());

                Game1.Reset();
            }
            base.Update(delta);
        }

        public override void Draw()
        {
            Globals.Globals.spriteBatch.Begin();
            Globals.Globals.spriteBatch.Draw(gameWinScreen, new Rectangle(0, 0, Game1.GAME_SIZE_X, Game1.GAME_SIZE_Y), Color.White);
            Globals.Globals.spriteBatch.End();
            base.Draw();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public enum options { ATTACK, REPAIR, BAIL }

namespace DayofVictory.ScreenManager.Screens
{
    class HUDScreen : BaseScreen
    {
        private Vector2 MenuSize = new Vector2(250, 160);

    //private Vector2 MenuPos = new Vector2( Globals.GameSize.X / 2, Globals.GameSize.Y / 3)


        public HUDScreen()
        {
            name = "HUDScreen";
            state = ScreenState.Active;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
        }

        public override void Draw()
        {
            base.Draw();
            Globals.Globals.spriteBatch.Begin();
            //Enemy health bar and fill
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.selectbar, new Rectangle(0, 0, 100, 10), new Rectangle(64, 0, 64, 64), Color.White);
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.water, new Rectangle(0, 0, Game1.enemyShip.WaterTaken() / Ship.MAX_WATER, 10), Color.White);

            //Friendly health bar and fill
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.selectbar, new Rectangle((int)Globals.Globals.gameSize.X - 120, 0, 100, 10), new Rectangle(64, 0, 64, 64), Color.White);
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.water, new Rectangle(0, 0, Game1.playerShip.WaterTaken() / Ship.MAX_WATER, 10), Color.White);

            //Overlay. Could make this a second screen with it's own handle input.
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.overlay, new Rectangle(0, (int)Globals.Globals.gameSize.Y / 3, 160, 160), Color.White);


            Globals.Globals.spriteBatch.End();

        }

    }
}

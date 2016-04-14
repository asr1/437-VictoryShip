using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


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


        }

    }
}

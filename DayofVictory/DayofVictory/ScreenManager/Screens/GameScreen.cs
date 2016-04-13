using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayofVictory.ScreenManager.Screens
{
    class GameScreen : BaseScreen
    {
        public GameScreen()
        {
            name = "GameScreen";
            state = ScreenState.Active;
        }

        public override void Update()
        {
            //Keep the HUDScreen on top. //Is this necessary, strictly speaking?
            //Like, it clearly needs to be on top. But wouldn't it be sufficient
            //To set it on top during the constructor, and not EVERY SINGLE FRAME?
            ScreenManager.unloadScreen("HUDScreen");
            ScreenManager.addScreen(new HUDScreen());
        }
    }
}

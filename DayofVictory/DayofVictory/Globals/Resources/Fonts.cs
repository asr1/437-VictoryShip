using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


//The Resources namespace should be used for 
//Fonts, sounds, textures, and models, and should be
//Set up like this, with static references loaded in a load(),
//Which is called is game1.load()
namespace DayofVictory.Globals.Resources
{
    class Fonts
    {
          public static SpriteFont Georgia_16;

        public static void load()
        {
            Georgia_16 = Globals.content.Load<SpriteFont>("fonts/Georgia_16");
        }
    }
}

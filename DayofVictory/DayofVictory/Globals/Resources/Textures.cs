using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DayofVictory.Globals.Resources
{
    class Textures
    {
        public static Texture2D overlay;
        public static Texture2D rightArrow;
        public static Texture2D water;
        public static Texture2D selectbar;

        public static void load()
        {
            overlay = Globals.content.Load<Texture2D>("textures/overlay");
            rightArrow = Globals.content.Load<Texture2D>("textures/rightarrow");
            water = Globals.content.Load<Texture2D>("textures/water");
            selectbar = Globals.content.Load<Texture2D>("textures/Selectbar");
        }

    }
}

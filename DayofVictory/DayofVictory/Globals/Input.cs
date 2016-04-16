using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

//Literally copy-pasted, again, from Alex's 437 Asteroid project
namespace DayofVictory.Globals
{
    public class Input
    {
        //Keyboard
        static KeyboardState currentKeyState;
        static KeyboardState lastKeyState;

        //Xbox 360 controller 1
        static GamePadState CurrentButtonstate1;
        static GamePadState LastButtonState1;

        public static void Update()
        {
            //Current to old
            lastKeyState = currentKeyState;
            LastButtonState1 = CurrentButtonstate1;

            //New to current
            currentKeyState = Keyboard.GetState();
            CurrentButtonstate1 = GamePad.GetState(PlayerIndex.One);
        }

        public static Boolean keyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static Boolean buttonDown(Buttons button, PlayerIndex controller)
        {
            switch (controller)
            {
                //Could easily add more controllers in the future.
                case PlayerIndex.One:
                    return CurrentButtonstate1.IsButtonDown(button);
            }
            return false;
        }

        public static Boolean keyPressed(Keys key)
        {
            if (currentKeyState.IsKeyDown(key) && lastKeyState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        public static Boolean buttonPressed(Buttons button, PlayerIndex controller)
        {
            switch (controller)
            {
                case PlayerIndex.One:
                    return CurrentButtonstate1.IsButtonDown(button) && LastButtonState1.IsButtonUp(button);
            }
            return false;
        }

    }
}

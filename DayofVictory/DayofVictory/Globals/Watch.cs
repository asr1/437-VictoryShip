using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DayofVictory
{
    class Watch
    {
        // Vars for our timer
        private static float timer;
        private static float timerS;
        private static float timerM;
        private static float timerH;
        private static int sec;
        private static int min;
        private static int hour;
        private static string secS;
        private static string minS;
        private static string hourS;

        // Update out game timer
        public void updTime(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds; // base timer used to return total ellapsed seconds

            timerS += (float)gameTime.ElapsedGameTime.TotalSeconds;
            sec += (int)timerS;
            sec %= 60;
            if (timerS >= 1.0F) timerS = 0F;
            if (sec < 10)
            {
                secS = "0";
                secS += sec.ToString();
            }
            else
            {
                secS = sec.ToString();
            }

            timerM += (float)gameTime.ElapsedGameTime.TotalMinutes;
            min += (int)timerM;
            min %= 60;
            if (timerM >= 1.0F) timerM = 0F;
            if (min < 10)
            {
                minS = "0";
                minS += min.ToString();
            }
            else
            {
                minS = min.ToString();
            }

            timerH += (float)gameTime.ElapsedGameTime.TotalHours;
            hour += (int)timerH;
            if (timerH >= 1.0F) timerH = 0F;
            if (hour < 10)
            {
                hourS = "0";
                hourS += hour.ToString();
            }
            else
            {
                hourS = hour.ToString();
            }
        }

        public string getTime()
        {
            return hourS + ":" + minS + ":" + secS;
        }

        // Return the ellapsed number of seconds in int format
        public int getEllapsedSec()
        {
            return (int)timer;
        }

        // Return the current second in string format
        public string getSecString()
        {
            return secS;
        }
    }
}

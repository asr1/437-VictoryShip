﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


public enum options { ATTACK, REPAIR, BAIL }

namespace DayofVictory.ScreenManager.Screens
{
    class HUDScreen : BaseScreen
    {
        private const int WATER_BAIL_AMOUNT = 10; //TODO CHange to something better
        private const int REPAIR_HOLES_AMOUNT = 1;

        private List<Utilities.MenuEntry> Entries = new List<Utilities.MenuEntry>();
        private options selection = options.ATTACK;
        private int triangleY;

        private Vicky vicky = new Vicky();
        private Enemy enemy = new Enemy();
        private Watch watch = new Watch();

        private static Vector2 menuSize = new Vector2(120, 100);
        private Vector2 menuPos = new Vector2(Globals.Globals.gameSize.X/2, Globals.Globals.gameSize.Y - menuSize.Y);

        private static Vector2 moveSize = new Vector2(200, 180);
        private Vector2 movePos = new Vector2(15, 200);
        private const int MOVE_OFFSET = 15;
        private int moveY = 0;

        private SoundEffect fireEffect;
        private SoundEffect repairEffect;
        private SoundEffect bailEffect;

        //private Vector2 MenuPos = new Vector2( Globals.GameSize.X / 2, Globals.GameSize.Y / 3)


        public HUDScreen()
        {
            name = "HUDScreen";
            state = ScreenState.Active;
            updateEntries();
            fireEffect = Globals.Globals.content.Load<SoundEffect>("sound/cannon");
            repairEffect = Globals.Globals.content.Load<SoundEffect>("sound/boardSound");
            bailEffect = Globals.Globals.content.Load<SoundEffect>("sound/bucketSound");
            SoundEffect.MasterVolume = 1f;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            updateEntries();
        }

        public override void Draw()
        {
            base.Draw();
            Globals.Globals.spriteBatch.Begin();
            //Enemy health bar and fill
            Globals.Globals.spriteBatch.DrawString(Globals.Resources.Fonts.Georgia_16, "Enemy Water Level: ", new Vector2(15, 10), Color.Black);
            Globals.Globals.spriteBatch.DrawString(Globals.Resources.Fonts.Georgia_16, "Holes: " + Game1.enemyShip.NumHoles(), new Vector2(60, 80), Color.Black);
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.water, new Rectangle (60, 45, Game1.enemyShip.WaterTaken(), 30), Color.White);
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.selectbar, new Rectangle(50, 45, 100, 30), new Rectangle(64, 0, 64, 64), Color.White);

            //Friendly health bar and fill
            Globals.Globals.spriteBatch.DrawString(Globals.Resources.Fonts.Georgia_16, "Player Water Level: ", new Vector2(Globals.Globals.gameSize.X - 280, Globals.Globals.gameSize.Y - 90), Color.Black);
            Globals.Globals.spriteBatch.DrawString(Globals.Resources.Fonts.Georgia_16, "Holes: " + Game1.playerShip.NumHoles(), new Vector2(Globals.Globals.gameSize.X - 230, Globals.Globals.gameSize.Y - 25), Color.Black);
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.water, new Rectangle((int)Globals.Globals.gameSize.X - 235, (int)Globals.Globals.gameSize.Y - 60, Game1.playerShip.WaterTaken(), 30), Color.White);
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.selectbar, new Rectangle((int)Globals.Globals.gameSize.X - 245, (int)Globals.Globals.gameSize.Y - 60, 100, 30), new Rectangle(64, 0, 64, 64), Color.White);

            //Recent moves
            moveY = 0;
            foreach (String s in Game1.recentMoves)
            {
                Globals.Globals.spriteBatch.DrawString(Globals.Resources.Fonts.Georgia_16, s, new Vector2(movePos.X + 8, movePos.Y + moveY + 10), Color.Black);
                moveY += MOVE_OFFSET + 5;
            }             
            //Recent moves gets an overlay too.
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.overlay, new Rectangle((int)movePos.X, (int)movePos.Y, (int)moveSize.X, (int)moveSize.Y + 50), Color.White);

            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.overlay, new Rectangle((int)menuPos.X, (int)menuPos.Y, (int)menuSize.X, (int)menuSize.Y + 50), Color.White);

            //Overlay. Could make this a second screen with it's own handle input.
            Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.overlay, new Rectangle((int)menuPos.X, (int)menuPos.Y, (int)menuSize.X, (int)menuSize.Y), Color.White);
            int menuY = (int)menuPos.Y + 20;

            for(int i = 0; i < Entries.Count; i++)
            {
                if (i == (int)selection)
                {
                    vicky.resetStates();
                    if ((int)selection == 0) vicky.setShooter();
                    else if ((int)selection == 1) vicky.setRepairing();
                    else if ((int)selection == 2) vicky.setBailing();

                    Globals.Globals.spriteBatch.Draw(Globals.Resources.Textures.rightArrow, new Rectangle((int)menuPos.X + 5, menuY - 2, 25, 25), Color.Red);
                }
                if (Entries[i].Enabled)
                {
                    Globals.Globals.spriteBatch.DrawString(Globals.Resources.Fonts.Georgia_16, Entries[i].Text, new Vector2(menuPos.X + 32, menuY), Color.Blue);

                }
                else
                {
                    Globals.Globals.spriteBatch.DrawString(Globals.Resources.Fonts.Georgia_16, Entries[i].Text, new Vector2(menuPos.X + 32, menuY), Color.Gray);

                }

                menuY += 20;
            }

            Globals.Globals.spriteBatch.End();

        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (Globals.Input.keyPressed(Keys.Up) || Globals.Input.keyPressed(Keys.W) || Globals.Input.buttonPressed(Buttons.DPadUp, PlayerIndex.One) || Globals.Input.buttonPressed(Buttons.LeftThumbstickUp, PlayerIndex.One))
            {
                do
                {
                    selection--;
                    if(selection < 0)
                    {
                        selection = (options)Entries.Count - 1;
                    }

                } while(Entries[(int)selection].Enabled == false);
            }

            if (Globals.Input.keyPressed(Keys.Down) || Globals.Input.keyPressed(Keys.S) || Globals.Input.buttonPressed(Buttons.DPadDown, PlayerIndex.One) || Globals.Input.buttonPressed(Buttons.LeftThumbstickDown, PlayerIndex.One))
            {
                do
                {
                    selection++;
                    if((int)selection > (Entries.Count - 1))
                    {
                        selection = 0;
                    }

                } while(Entries[(int)selection].Enabled == false);
            }

            if (Game1.IsPlayersTurn() && (Globals.Input.keyPressed(Keys.Enter) || Globals.Input.buttonPressed(Buttons.A, PlayerIndex.One)))
            {
                switch (selection)
                {
                    case options.ATTACK:
                        Game1.playerShip.FireShot(Game1.enemyShip);
                        Game1.recentMoves.Add("You shot the enemy");
                        enemy.setUnderAttack(watch);
                        fireEffect.Play();
                        break;
                    case options.BAIL:
                        Game1.playerShip.BailWater(WATER_BAIL_AMOUNT);
                        Game1.recentMoves.Add("You bailed water");
                        bailEffect.Play();
                        break;
                    case options.REPAIR:
                        Game1.playerShip.Repair(REPAIR_HOLES_AMOUNT);
                        Game1.recentMoves.Add("You fixed a hole");
                        repairEffect.Play();
                        break;
                }
                Game1.TrimRecentsList();
                Game1.setPlayersTurn(false);
            }
        }


        public void addEntry(String text, bool enabled)
        {
            Utilities.MenuEntry entry = new Utilities.MenuEntry();
            entry.Enabled = enabled;
            entry.Text = text;
            Entries.Add(entry);
        }

        public void updateEntries()
        {                                
            Entries.Clear();
            addEntry("Shoot", true);
            addEntry("Repair", Game1.playerShip.NumHoles() > 0);
            addEntry("Bail", Game1.playerShip.WaterTaken() > 0);
        }

    }



}

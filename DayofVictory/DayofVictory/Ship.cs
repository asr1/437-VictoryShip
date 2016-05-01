using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DayofVictory
{
    public class Ship : DrawableGameComponent
    {
        public const int WATER_PER_HOLE = 5;
        public const int WATER_PER_BAIL = 13;

        public const int MAX_WATER = 100;

        protected Game1 game;

        private int water;
        private int holes;

        private Texture2D texture;

        private Vector2 pos;

        public Ship(Game theGame, Texture2D texture) : base(theGame)
        {
            this.texture = texture;
            this.game = (Game1) theGame;
        }

        public void SetPosition(Vector2 pos)
        {
            this.pos = pos;
        }

        public void AddHole(int numHoles)
        {
            holes += numHoles;
        }

        public void FireShot(Ship target)
        {
            target.AddHole(1);
        }

        public void BailWater(int howMuch)
        {
            water = MathHelper.Max(0, water - howMuch);
        }

        public void Repair(int howMany)
        {
            holes = MathHelper.Max(0, holes - howMany);
        }

        public void TakeOnWater()
        {
            water += holes * WATER_PER_HOLE;
        }

        public int WaterTaken()
        {
            return water;
        }

        public int NumHoles()
        {
            return holes;
        }

        public bool IsAlive()
        {
            return water < MAX_WATER;
        }

        public override void Draw(GameTime gameTime)
        {
            //TODO SpriteBatch.Draw it
        }
    }
}

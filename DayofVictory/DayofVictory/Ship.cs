using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DayofVictory
{
    class Ship : DrawableGameComponent
    {
        private const int WATER_PER_HOLE = 1;

        public const int MAX_WATER = 100;

        private Game1 game;

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

        public void AddHole()
        {
            holes++;
        }

        public void TakeDamage()
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

        //public override void Draw()
        //{
        //    //TODO SpriteBatch.Draw it
        //}
    }
}

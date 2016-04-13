using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DayofVictory
{
    class Ship : DrawableGameComponent
    {
        private const int WATER_PER_HOLE = 1;

        private const int MAX_WATER = 100;

        private int water;
        private int holes;

        private Texture2D texture;

        private Vector2 pos;

        public Ship(Game game, Texture2D texture) : base(game)
        {
            this.texture = texture;
        }

        public void SetPosition(Vector2 pos)
        {
            this.pos = pos;
        }

        public void AddHole()
        {
            holes++;
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(gameTime.ElapsedGameTime.Seconds >= 1)
            {
                water += holes * WATER_PER_HOLE;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DayofVictory.AI
{
    public class AIShip : Ship
    {
        private AIShipCalculator calc;
        private Ship opponent;

        public AIShip(Game1 theGame, Texture2D texture, Ship opponent)
            : base(theGame, texture)
        {
            this.opponent = opponent;
            calc = new AIShipCalculator(Ship.MAX_WATER, Ship.MAX_WATER);
        }

        public void DoMove()
        {
            AIMove move = calc.Calculate(WaterTaken(), opponent.WaterTaken(), NumHoles());
            switch (move)
            {
                case AIMove.FIRE:
                    FireShot(opponent);
                    break;
                case AIMove.BOARD:
                    Repair(1);
                    break;
                case AIMove.BUCKET:
                    BailWater(Ship.WATER_PER_BAIL);
                    break;
            }
        }
    }
}

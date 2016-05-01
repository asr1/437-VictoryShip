using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DayofVictory.AI
{
    public class AIShip : Ship
    {
        private AIShipCalculator calc;
        private Ship opponent;
        Vicky vicky = new Vicky();
        Watch watch = new Watch();

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
                    vicky.setUnderAttack(watch);
                    Game1.recentMoves.Add("AI fired a shot");
                    break;
                case AIMove.BOARD:
                    Repair(1);
                    Game1.recentMoves.Add("AI fixed a hole");
                    break;
                case AIMove.BUCKET:
                    BailWater(Ship.WATER_PER_BAIL);
                    Game1.recentMoves.Add("AI bailed water");
                    break;
            }
            Game1.TrimRecentsList();
        }
    }
}

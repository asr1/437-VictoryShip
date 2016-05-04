using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
namespace DayofVictory.AI
{
    public class AIShip : Ship
    {
        private AIShipCalculator calc;
        private Ship opponent;
        Vicky vicky = new Vicky();
        Watch watch = new Watch();
        private SoundEffect fireEffect;
        private SoundEffect repairEffect;
        private SoundEffect bailEffect;

        public AIShip(Game1 theGame, Texture2D texture, Ship opponent)
            : base(theGame, texture)
        {
            this.opponent = opponent;
            calc = new AIShipCalculator(Ship.MAX_WATER, Ship.MAX_WATER);
            fireEffect = Globals.Globals.content.Load<SoundEffect>("sound/cannon");
            repairEffect = Globals.Globals.content.Load<SoundEffect>("sound/boardSound");
            bailEffect = Globals.Globals.content.Load<SoundEffect>("sound/bucketSound");
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
                    fireEffect.Play();
                    break;
                case AIMove.BOARD:
                    Repair(1);
                    Game1.recentMoves.Add("AI fixed a hole");
                    repairEffect.Play();
                    break;
                case AIMove.BUCKET:
                    BailWater(Ship.WATER_PER_BAIL);
                    Game1.recentMoves.Add("AI bailed water");
                    bailEffect.Play();
                    break;
            }
            Game1.TrimRecentsList();
        }
    }
}

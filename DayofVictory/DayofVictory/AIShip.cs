using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayofVictory
{
    public enum AIMove 
    {
        FIRE,
        BOARD,
        BUCKET
    }

    public class AIShipCalculator
    {
        private Random rand;
        private int maxWaterAI;
        private int maxWaterOpponent;

        /// <summary>
        /// Pass in maximum water the AI can take and maximum water the opponent can take.
        /// Note: Opponent should be easier to kill if its max water is much less than opponents
        /// </summary>
        /// <param name="maxWaterAI"></param>
        /// <param name="maxWaterOpponent"></param>
        public AIShipCalculator(int maxWaterAI, int maxWaterOpponent)
        {
            rand = new Random();
            this.maxWaterAI = maxWaterAI;
            this.maxWaterOpponent = maxWaterOpponent;
        }

        /// <summary>
        /// Returns type of move opponent will make.
        /// If opponent is over 85% water, it will fire every time to destroy them!
        /// </summary>
        /// <param name="water"></param>
        /// <param name="opponentWater"></param>
        /// <param name="numHoles"></param>
        /// <returns></returns>
        public AIMove Calculate(int water, int opponentWater, int numHoles)
        {
            float percentOpponent = (float) opponentWater / maxWaterOpponent;
            float percentUs = (float) water / maxWaterAI;
            if (percentUs > 0.85)
            {
                if (percentOpponent > 0.85)
                    return AIMove.FIRE;
                else if (rand.Next(2) == 0)
                    return AIMove.FIRE;
                else
                    return AIMove.BUCKET;
            }
            if (percentUs > 0.75 && numHoles > 0)
            {
                if (rand.Next(2) == 0)
                    return AIMove.FIRE;
                else
                    return AIMove.BOARD;
            }
            if (percentUs > 0 && numHoles > 0) 
            {
                int random = rand.Next(3);
                if (random == 0)
                    return AIMove.FIRE;
                else if (random == 1)
                    return AIMove.BOARD;
                else
                    return AIMove.BUCKET;
            }
            if (percentUs > 0 && numHoles == 0) {
                if (rand.Next(2) == 0)
                    return AIMove.FIRE;
                else
                    return AIMove.BUCKET;
            }
            if (numHoles > 0)
                if (rand.Next(2) == 0)
                    return AIMove.FIRE;
                else
                    return AIMove.BOARD;
            return AIMove.FIRE;
        }
    }
}

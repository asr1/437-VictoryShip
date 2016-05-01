using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class is responsible for dealing with Vicky's states
namespace DayofVictory
{
    class Vicky 
    {
        private static bool underAttack = false;
        private static bool shooting;
        private static bool repairing;
        private static bool bailing;
        private static int vickyHurtIconInitialized;

        public void setShooter()
        {
            shooting = true;
        }

        public bool isShooting()
        {
            return shooting;
        }

        public void setRepairing()
        {
            repairing = true;
        }

        public bool isRepairing()
        {
            return repairing;
        }

        public void setBailing()
        {
            bailing = true;
        }

        public bool isBailing()
        {
            return bailing;
        }

        public void setUnderAttack(Watch watch)
        {
            underAttack = true;
            vickyHurtIconInitialized = watch.getEllapsedSec();
        }

        public bool isUnderAttack()
        {
            return underAttack;
        }

        // Changes Vicky's display icons according to Vicky's states 
        // Icon changes back after 1 sec
        public void hurtIconCheck(Watch watch)
        {
            if (underAttack && (watch.getEllapsedSec() - vickyHurtIconInitialized) >= 1)
            {
                underAttack = false;
            }
        }

        // Reset Vicky's action states
        public void resetStates()
        {
            repairing = false;
            bailing = false;
            shooting = false;
        }
    }
}

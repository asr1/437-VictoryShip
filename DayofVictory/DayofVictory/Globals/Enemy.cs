using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayofVictory
{
    class Enemy
    {
        private static bool underAttack = false;
        private static int hurtIconInitialized;

        public void setUnderAttack(Watch watch)
        {
            underAttack = true;
            hurtIconInitialized = watch.getEllapsedSec();
        }

        public bool isUnderAttack()
        {
            return underAttack;
        }

        public void hurtIconCheck(Watch watch)
        {
            if (underAttack && (watch.getEllapsedSec() - hurtIconInitialized) >= 2)
            {
                underAttack = false;
            }
        }
    }
}

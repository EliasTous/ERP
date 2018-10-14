using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AionHR.Web.UI.Forms.ConstClasses
{
    public static class ConstTimeVariationType
    {
        // allowed leave 1x
        public const short UNPAID_LEAVE = 11;
        public const short PAID_LEAVE = 12;

        // day without excuse 2x
        public const short SHIFT_LEAVE_WITHOUT_EXCUSE = 21;
        public const short DAY_LEAVE_WITHOUT_EXCUSE = 22;

        // lateness 3x
        public const short LATE_CHECKIN = 31;
        public const short DURING_SHIFT_LEAVE = 32;
        public const short EARLY_LEAVE = 33;

        //  missed punch 4x
        public const short MISSED_PUNCH = 41;

        // overtime 5x
        public const short EARLY_CHECKIN = 51;
        public const short OVERTIME = 52;
        public const short Day_Bonus = 53;

        public const short COUNT = 10;
       
    }

}
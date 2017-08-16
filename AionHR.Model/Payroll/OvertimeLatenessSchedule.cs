using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
   public class OvertimeLatenessSchedule:ModelBase
    {
        public string name { get; set; }
        public bool? aEnable { get; set; }
        public int? aPeriodDays { set; get; }
        public double? aMultiplier { get; set; }
        public bool? mEnable { get; set; }
        public int? mPeriodDays { set; get; }
        public double? mMultiplier { get; set; }
        public bool? oEnable { get; set; }
        public int? oPeriodHours { set; get; }
        public double? oMultiplier { get; set; }
        public bool? lEnable { get; set; }
        public int? lPeriodHours { set; get; }
        public double? lMultiplier { get; set; }
        public int? lAllowance { get; set; }
        public bool? dEnable { get; set; }
        public int? dPeriodDays { set; get; }
        public double? dMultiplier { get; set; }



    }
}

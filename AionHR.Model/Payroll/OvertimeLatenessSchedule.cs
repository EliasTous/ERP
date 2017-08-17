using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51030","51")]
   public class OvertimeLatenessSchedule:ModelBase
    {
        [PropertyID("51030_01")]
        public string name { get; set; }
        [PropertyID("51030_02")]
        public bool? aEnable { get; set; }
        [PropertyID("51030_02")]
        public int? aPeriodDays { set; get; }
        [PropertyID("51030_02")]
        public double? aMultiplier { get; set; }
        [PropertyID("51030_03")]
        public bool? mEnable { get; set; }
        [PropertyID("51030_03")]
        public int? mPeriodDays { set; get; }
        [PropertyID("51030_03")]
        public double? mMultiplier { get; set; }
        [PropertyID("51030_04")]
        public bool? oEnable { get; set; }
        [PropertyID("51030_04")]
        public int? oPeriodHours { set; get; }
        [PropertyID("51030_04")]
        public double? oMultiplier { get; set; }
        [PropertyID("51030_05")]
        public bool? lEnable { get; set; }
        [PropertyID("51030_05")]
        public int? lPeriodHours { set; get; }
        [PropertyID("51030_05")]
        public double? lMultiplier { get; set; }
        [PropertyID("51030_05")]
        public int? lAllowance { get; set; }
        [PropertyID("51030_06")]
        public bool? dEnable { get; set; }
        [PropertyID("51030_06")]
        public int? dPeriodDays { set; get; }
        [PropertyID("51030_06")]
        public double? dMultiplier { get; set; }
       



    }
}

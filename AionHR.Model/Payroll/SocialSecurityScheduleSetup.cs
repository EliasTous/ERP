using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{

    [ClassIdentifier("51050", "50")]
    public class SocialSecurityScheduleSetup
    {
        [PropertyID("51050_01")]
        [ApplySecurity]
        public int? ssId { get; set; }
        [PropertyID("51050_02")]
        [ApplySecurity]
        public int? seqNo { get; set; }
        [PropertyID("51050_03")]
        [ApplySecurity]
        public string payCode { get; set; }
        [PropertyID("51050_04")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("51050_05")]
        [ApplySecurity]
        public double coPct { get; set; }
        [PropertyID("51050_06")]
        [ApplySecurity]
        public double emPct { get; set; }
        [PropertyID("51050_07")]
        [ApplySecurity]
        public  int? ceiling { set; get; }
    }
}

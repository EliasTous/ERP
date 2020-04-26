using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Payroll
{

    [ClassIdentifier("51015", "51")]
    public class SocialSecurityScheduleSetup
    {
        [PropertyID("51015_01")]
        [ApplySecurity]
        public int? ssId { get; set; }
        [PropertyID("51015_02")]
        [ApplySecurity]
        public int? seqNo { get; set; }
        [PropertyID("51015_03")]
        [ApplySecurity]
        public string payCode { get; set; }
        [PropertyID("51015_04")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("51015_05")]
        [ApplySecurity]
        public double coPct { get; set; }
        [PropertyID("51015_06")]
        [ApplySecurity]
        public double emPct { get; set; }
        [PropertyID("51015_07")]
        [ApplySecurity]
        public  int? ceiling { set; get; }
    }
}

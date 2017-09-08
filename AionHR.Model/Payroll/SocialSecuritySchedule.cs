using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51014", "51")]
    public class SocialSecuritySchedule: ModelBase
    {
        
      [ PropertyID("51014_1")]
        [ApplySecurity]
        public string name { get; set; }
        public double coPct { get; set; }
        public double emPct { get; set; }
    }
}

using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.NationalQuota
{
    [ClassIdentifier("10105", "10")]
    public   class PointAcquisition
    {
        [PropertyID("10105_01")]
        [ApplySecurity]
        public int days { get; set; }
        [PropertyID("10105_02")]
        [ApplySecurity]
        public double hiredPct { get; set; }
        [PropertyID("10105_03")]
        [ApplySecurity]
        public double terminatedPct { get; set; }
      
    }
}

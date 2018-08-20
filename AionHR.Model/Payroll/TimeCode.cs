using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51013","51")]
 public class TimeCode 
    {
        public int tsId { get; set; }
        [PropertyID("51013_01")]
        [ApplySecurity]
        public string timeCode { get; set; }
        [PropertyID("51013_01")]
        [ApplySecurity]
        public bool isEnabled { get; set; }
        [PropertyID("51013_01")]
        [ApplySecurity]
        public short deductPeriod { get; set; }
        [PropertyID("51013_01")]
        [ApplySecurity]
        public short fullPeriod { get; set; }
        [PropertyID("51013_01")]
        [ApplySecurity]
        public double multiplier { get; set; }

         public string apId { get; set; }
        public string apName { get; set; }
        public short maxAllowed { get; set; }
        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
 public class TimeCode 
    {
        public int tsId { get; set; }
        public string timeCode { get; set; }
        public bool isEnabled { get; set; }
        public short deductPeriod { get; set; }
        public short fullPeriod { get; set; }
        public double multiplier { get; set; }


    }
}

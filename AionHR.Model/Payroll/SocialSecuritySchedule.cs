using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
   public class SocialSecuritySchedule: ModelBase
    {
        public string name { get; set; }
        public double coPct { get; set; }
        public double emPct { get; set; }
    }
}

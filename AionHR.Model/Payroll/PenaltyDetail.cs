using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
  public  class PenaltyDetail :ModelBase
    {
        public string ptId { get; set; }
        public short damage  { get; set; }
        public int sequence { get; set; }
        public short action { get; set; }
        public double pct { get; set; }



    }
}

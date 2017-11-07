using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
  public  class PayrollIndemnityRecognition
    {
        public int from { get; set; }
        public int to { get; set; }
        public double pct { get; set; }
        public int inId { get; set; }
        public int seqNo { get; set; }
    }
}

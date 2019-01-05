using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51019", "20")]
    public  class PayrollIndemnityRecognition
    {
        public int from { get; set; }
        public int to { get; set; }
        public double pct { get; set; }
        public int inId { get; set; }
        public int seqNo { get; set; }
    }
}

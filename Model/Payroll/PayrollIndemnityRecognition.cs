﻿using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Payroll
{
    [ClassIdentifier("51019", "51")]
    public  class PayrollIndemnityRecognition
    {
        public int from { get; set; }
        public int to { get; set; }
        public double pct { get; set; }
        public int inId { get; set; }
        public int seqNo { get; set; }
    }
}

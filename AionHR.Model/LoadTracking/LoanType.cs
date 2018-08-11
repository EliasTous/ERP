﻿using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LoadTracking
{
    [ClassIdentifier("45000", "45")]
    public class LoanType: ModelBase
    {
        [PropertyID("45000_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("45000_02")]
        [ApplySecurity]
        public int ldMethod { get; set; }
        [PropertyID("45000_03")]
        [ApplySecurity]
        public double ldValue { get; set; }
        [PropertyID("45000_04")]
        [ApplySecurity]
        public bool disableEditing { get; set; }
        
    }
}

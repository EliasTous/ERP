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
        public string name { get; set; }
    }
}

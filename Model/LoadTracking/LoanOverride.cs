﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.LoadTracking
{
    [ClassIdentifier("45010", "45")]
    public class LoanOverride
    {
        [PropertyID("45010_01")]
        public string name { get; set; }

        [PropertyID("45010_01")]
        public string employeeId { get; set; }
        [PropertyID("45010_02")]
        public string ldMethod { get; set; }
        [PropertyID("45010_03")]
        public string ldValue { get; set; }
    }
}

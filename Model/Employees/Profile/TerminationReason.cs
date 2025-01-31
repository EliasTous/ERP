﻿using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31130", "31")]
    public class TerminationReason:ModelBase
    {
        [PropertyID("31130_01")]
        [ApplySecurity]
        public string name { get; set; }
        public short penaltyStatus{ get; set; }
    }
}

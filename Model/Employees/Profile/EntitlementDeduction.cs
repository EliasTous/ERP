﻿using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31061", "31")]
    public class EntitlementDeduction:ModelBase
    {
        [PropertyID("31061_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("31061_02")]
        [ApplySecurity]
        public short type { get; set; }
        [PropertyID("31061_03")]
        [ApplySecurity]
        public string paycodeRef { get; set; }
        [PropertyID("31061_04")]
        [ApplySecurity]
        public bool isTaxable { get; set; }
        public string expressionId { get; set; }
    }
}

﻿using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Attendance
{
    [ClassIdentifier("41010", "41")]
    public class Router:ModelBase
    {
        [PropertyID("41010_01")]
        [ApplySecurity]
        public string routerRef { get; set; }
        [PropertyID("41010_02")]
        [ApplySecurity]
        public short activeStatus { get; set; }
        [PropertyID("41010_03")]
        [ApplySecurity]
        public int branchId { get; set; }

        [PropertyID("41010_03")]
        [ApplySecurity]
        public string branchName { get; set; }
    }
}

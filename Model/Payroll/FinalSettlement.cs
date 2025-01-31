﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Payroll
{
    [ClassIdentifier("51030", "51")]
    public class FinalSettlement:ModelBase
    {
        [PropertyID("51030_07")]
        [ApplySecurity]
        public string employeeName { get; set; }
        [PropertyID("51030_08")]
        [ApplySecurity]
        public string fsRef { get; set; }
        [PropertyID("51030_09")]
        [ApplySecurity]
        public DateTime date { get; set; }
        public string dateStringFormat { get; set; }
        public string employeeId { get; set; }

    }
}

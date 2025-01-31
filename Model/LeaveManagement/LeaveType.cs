﻿using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Leaves
{
    [ClassIdentifier("42000", "42")]
    public class LeaveType:ModelBase
    {
        [PropertyID("42000_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("42000_02")]
        [ApplySecurity]
        public string reference { get; set; }
        //[PropertyID("42000_03")]
        //[ApplySecurity]
        //public bool requireApproval { get; set; }

        [PropertyID("42000_04")]
        [ApplySecurity]
        public int leaveType { get; set; }
        [PropertyID("42000_05")]
        [ApplySecurity]
        public bool isPaid { get; set; }
        [PropertyID("42000_06")]
        [ApplySecurity]
        public int? edId { get; set; }
        //public bool raReportTo { get; set; }
        //[PropertyID("42000_06")]
        //[ApplySecurity]
        //public bool raDepHead { get; set; }
        //[PropertyID("42000_06")]
        //[ApplySecurity]
        //public bool raDepHierarchy { get; set; }
        //[PropertyID("42000_06")]
        //[ApplySecurity]
        //public bool raDepLA { get; set; }
        //[PropertyID("42000_07")]
        //[ApplySecurity]
        //public bool raBrHead{ get; set; }
        public int? apId { get; set; }
        public string apName { get; set; }
        public string lsId { get; set; }
    }
}

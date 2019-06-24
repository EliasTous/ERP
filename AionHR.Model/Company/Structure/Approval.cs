﻿using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
    [ClassIdentifier("21050", "21")]
    public  class Approval:ModelBase 
    {
        public string name { get; set; }
        public bool branchHead { get; set; }
        public bool departmentHead { get; set; }
        public bool reportTo { get; set; }
        public bool departmentTree { get; set; }
        public bool departmentList { get; set; }
        public short approvalType { get; set; }
    }
}

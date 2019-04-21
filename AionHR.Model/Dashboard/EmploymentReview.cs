﻿using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
    [ClassIdentifier("81110", "81")]
    public   class EmploymentReview
    {
        public EmployeeName name { get; set; }
        public string employeeName { get; set; }
        
        public DateTime probationEndDate { get; set; }
        public int days { get; set; }
        public DateTime nextReviewDate { get; set; }
        public DateTime termEndDate { get; set; }
        public string npName { get; set; }


    }
}

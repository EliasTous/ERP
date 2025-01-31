﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
    [ClassIdentifier("81111", "81")]
    public  class TermEndDate
    {
        public string   employeeName { get; set; }

        public DateTime probationEndDate
        {
            get; set;
        }
        public DateTime nextReviewDate
        {
            get; set;
        }
        public DateTime termEndDate
        {
            get; set;
        }

        public string npName { get; set; }
        public int days { get; set; }
    }
}

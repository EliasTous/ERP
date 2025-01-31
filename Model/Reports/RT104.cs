﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80104", "80")]
    public class RT104
    {
        public string employeeName
        {
            get; set;
        }

        public string employeeId { get; set; }

        public int hiredMonth { get; set; }
        public string HiredMonthString { get;
            set; }
        public int hiredDay { get; set; }
         public int yearsInService { get; set; }

        public DateTime? hireDate { get; set; }
        public string hireDateString { get; set; }

        public double indemnity { get; set; }

        public double salary { get; set; }

    }
}

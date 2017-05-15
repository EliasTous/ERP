﻿using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    public class RT601
    {
        public EmployeeName employeeName { get; set; }

        public DateTime startDate { get; set; }

        public String startDateString { get; set; }
        public DateTime endDate { get; set; }
        public String endDateString { get; set; }
        public DateTime? returnDate { get; set; }
        public String returnDateString { get; set; }
        public int leavePeriod { get; set; }
        public string justification { get; set; }

        public int status { get; set; }

        public string statusString { get; set; }
        public string destination { get; set; }
        public bool isPaid { get; set; }

        public string isPaidString { get; set; }
        public string ltName { get; set; }


    }
}

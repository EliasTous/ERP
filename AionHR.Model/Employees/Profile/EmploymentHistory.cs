﻿using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31071", "31")]
    public class EmploymentHistory :ModelBase
    {
    

        public int statusId { get; set; }

 

        public DateTime date { get; set; }

        public string comment { get; set; }
        public int employeeId { get; set; }

        public EmployeeName employeeName { get; set; }
        public string statusName { get; set; }
    }

    [ClassIdentifier("31070", "31")]
    public  class EmploymentStatus : ModelBase

    {
        public string name { get; set; }
    }
}

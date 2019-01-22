﻿using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
 public  class RT804
    {
        public EmployeeName employeeName { get; set; }
        public string branchName { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }
        public string userTypeName { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public bool isInactive { get; set; }
        

    }
}

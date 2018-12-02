﻿using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LeaveManagement
{
   public class Approvals
    {
        public EmployeeName employeeName { get; set; }
        public string departmentName { get; set; }
        public int leaveId { get; set; }
        public int approverId { get; set; }
        public int status { get; set; }
        public string notes { get; set; }
        public string stringStatus { set; get; }
        public DateTime startDate { set; get; }
        public DateTime endDate { set; get; }
        public string ltName { set; get; }

        public string branchName { get; set; }

    }
}

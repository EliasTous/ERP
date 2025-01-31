﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.LeaveManagement
{
    [ClassIdentifier("21050", "21")]

    public class Approvals
    {
        public string employeeName { get; set; }
        public string approverName { get; set; }
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
        public string arName { get; set; }
        public string arId { get; set; }
        public string seqNo { get; set; }
        public string activityId { get; set; }
        
    }
}

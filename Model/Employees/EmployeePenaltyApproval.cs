﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees
{
    [ClassIdentifier("31171", "31")]
    public class EmployeePenaltyApproval
    {
        public string approverName { get; set; }
        public string employeeName { get; set; }
        public string departmentName { get; set; }
        public int penaltyId { get; set; }
        public DateTime date{ get; set; }
        public string penaltyName { get; set; }
        public int approverId { get; set; }
        public short status { get; set; }
        public string notes { get; set; }
        public string statusString { get; set; }
        public string arId { get; set; }
        public string arName { get; set; }
        public string seqNo { get; set; }
        public string amount { get; set; }
        public string activityId { get; set; }

    }
}

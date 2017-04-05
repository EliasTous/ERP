using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeQuickView:ModelBase
    {
        public string esName { get; set; }
        public EmployeeName name { get; set; }
        public string positionName { get; set; }
        public string departmentName { get; set; }
        public string divisionName { get; set; }
        public string branchName { get; set; }
        public EmployeeName reportToName { get; set; }
        public int eosBalance { get; set; }
        public DateTime lastLeaveStartDate { get; set; }
        public DateTime lastLeaveEndDate { get; set; }
        public short paidLeavesYTD { get; set; }
        public short leavesBalance { get; set; }
        public short allowedLeaveYtd { get; set; }
    }
}

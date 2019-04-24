using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees
{
    [ClassIdentifier("31171", "31")]
    public class EmployeePenaltyApproval
    {
        public EmployeeName approverName { get; set; }
        public EmployeeName employeeName { get; set; }
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
        public string penaltyName { get; set; }
        public DateTime date { get; set; }
    }
}

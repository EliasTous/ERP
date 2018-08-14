using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
   public class LeavePayment:ModelBase
    {
        public EmployeeName employeeName { set; get; }
        public DateTime date { get; set; }

        public int employeeId { get; set;  }
        public DateTime effectiveDate { get; set; }
        public string paymentRef { get; set; }
        public int salary { get; set; }
        public double days { get; set; }
        public int amount { get; set; }
        public double earnedLeaves { get; set; }
        public double usedLeaves { get; set; }
        public double paidLeaves { get; set; }
        public double leaveBalance { get; set; }
        public int postingStatus { get; set; }
        public string dateString { get; set; }
        public string effectiveDateString { get; set; }
    }
}

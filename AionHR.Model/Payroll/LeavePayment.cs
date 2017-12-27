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
        public int days { get; set; }
        public int amount { get; set; }
        public int earnedLeaves { get; set; }
        public int usedLeaves { get; set; }
        public int paidLeaves { get; set; }
        public int leaveBalance { get; set; }
        public int postingStatus { get; set; }
        public string dateString { get; set; }
        public string effectiveDateString { get; set; }
    }
}

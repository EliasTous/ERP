using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    public class RT302
    {
        public EmployeeName name { get; set; }
        public string calendarHours { get; set; }
        public string workingHours { get; set; }
        public string totalLateness { get; set; }
        public string approvedLeaveHours { get; set; }
        public string unapprovedLeaveHours { get; set; }
        public string netSalary { get; set; }
        public string deductedUnpproved { get; set; }
        public string deductedApproved { get; set; }
        public string deductedAmount { get; set; }

        public string departmentName { get; set; }

        public string branchName { get; set; }
    }
}

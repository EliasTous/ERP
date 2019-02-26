using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
   public class PT501
    {
        public string ltName { get; set; }
        public string leaveId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public double? leaveDays { get; set; }
        public EmployeeName employeeName { get; set; }
        public string countryName { get; set; }
        public DateTime? hireDate { get; set; }
        public string positionName { get; set; }
        public string departmentName { get; set; }

        public string branchName { get; set; }

        public double? basicSalary { get; set; }
        public double? netSalary { get; set; }
        public double? leaveBalance { get; set; }
        public string lastLeave_ltName { get; set; }
        public DateTime? lastLeave_startDate { get; set; }
        public DateTime? lastLeave_endDate { get; set; }

        public int? currentLeaveLegDays { get; set; }

    }
}

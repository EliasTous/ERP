using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeQuickView : ModelBase
    {
        public string pictureUrl
        {
            get; set;
        }

        public string pictureFileName { get; set; }

        public string esName { get; set; }
        public EmployeeName name { get; set; }
        public string positionName { get; set; }
        public string departmentName { get; set; }
        public string divisionName { get; set; }
        public string branchName { get; set; }

        public EmployeeName reportToName { get; set; }
        public int indemnity { get; set; }
        public DateTime? lastLeaveStartDate { get; set; }
        public DateTime? lastLeaveEndDate { get; set; }
        public short usedLeavesLeg { get; set; }

        public string serviceDuration { get; set; }
        public short leaveBalance { get; set; }
        public short earnedLeavesLeg { get; set; }

        public string LastLeave(string format)
        {

            if (!lastLeaveEndDate.HasValue || !lastLeaveStartDate.HasValue)
                return "/";
            return lastLeaveStartDate.Value.ToString(format) + "-" + lastLeaveStartDate.Value.ToString(format);
        }
        public string serviceDuractionFriendly(string day, string month, string year)
        {
            if (string.IsNullOrEmpty(serviceDuration))
                return "";
             string yeard = serviceDuration.Replace("y", year);
            string monthd = yeard.Replace("m", month);
            return monthd.Replace("d", day);

           
        }
        public string countryName { get; set; }
        public DateTime? hireDate { get; set; }
        public int earnedLeaves { get;  set; }
        public int usedLeaves { get; set;  }
        public int paidLeaves { get; set; }
        public int salary { get; set; }

    }
}

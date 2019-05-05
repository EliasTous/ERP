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

        public string reportToName { get; set; }
        public int? indemnity { get; set; }
        public DateTime? lastLeaveStartDate { get; set; }
        public DateTime? lastLeaveEndDate { get; set; }
        public double? usedLeavesLeg { get; set; }

        public string serviceDuration { get; set; }
        public double? leaveBalance { get; set; }
        public short? earnedLeavesLeg { get; set; }

        public string LastLeave(string format)
        {

            if (!lastLeaveEndDate.HasValue || !lastLeaveStartDate.HasValue)
                return "/";
            return lastLeaveStartDate.Value.ToString(format) + "-" + lastLeaveEndDate.Value.ToString(format);
        }
        public string serviceDuractionFriendly(string day, string month, string year,string language)
        {
            string yeard;
            string monthd; 
            if (string.IsNullOrEmpty(serviceDuration))
                return "";
            if(language=="fr")
            {
               yeard = serviceDuration.Replace("an", year);
               
                monthd = yeard.Replace("m", month);
                return monthd.Replace("j", day);
            }
         
               yeard = serviceDuration.Replace("y", year);
              monthd = yeard.Replace("m", month);
            return monthd.Replace("d", day);

           
        }
        public string countryName { get; set; }
        public DateTime? hireDate { get; set; }
        public double? earnedLeaves { get;  set; }
        public double? usedLeaves { get; set;  }
        public double? paidLeaves { get; set; }
        public int? salary { get; set; }
        public string currencyName { get; set; }
        public short? status { get; set; }
        public string statusString { get ; set; }
        public int? loanBalance { get; set; }
        public DateTime? terminationDate { get; set; }
        public double? unpaidLeaves { get; set; }

        public string departmentId { get; set; }
        public string branchId { get; set; }


    }
}

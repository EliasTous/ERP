using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80108", "80")]
    public class RT108
    {
        

        public EmployeeName name { get; set; }

        public int religion { get; set; }

        public string religionString { get; set; }

        public string countryName { get; set; }

        public int gender { get; set; }

        public string genderString { get; set; }

        public string resRef { get; set; }

        public string resExpiryDate { get; set; }

        public string idExpiryString { get; set; }

        public string passportRef { get; set; }

        public string passportExpiryDate { get; set; }

        public string passportExpiryString { get; set; }

        public string positionName { get; set; }

        public string branchName { get; set; }

        public string departmentName { get; set; }
        public string divisionName { get; set; }
        public string ehStatus { get; set; }
        public DateTime? hireDate { get; set; }
        public string hireDateString { get; set; }
        public DateTime? lastLeaveReturnDate { get; set; }
        public string lastLeaveReturnString { get; set; }

        public string hireLength { get; set; }

        public DateTime hireEndDate { get; set; }
        public string hireEndDateString { get; set; }
        public string contractDuration { get; set; }
        public double taxableSalary { get; set; }
        public double unTaxableSalary { get; set; }

        public double finalSalary { get; set; }
        public bool isInactive { get; set; }

        public string isInactiveString { get; set; }
        public DateTime? terminationDate { get; set; }
        public string terminationDateString { get; set; }
        public string terminationReasonName { get; set; }

        public DateTime? termEndDate { get; set; }

        public string termEndDateString { get; set; }
        public double salary { get; set; }

        public string sponserName { get; set; }
    }
}

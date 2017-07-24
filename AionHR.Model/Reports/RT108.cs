using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    public class RT108
    {
        public string reference { get; set; }

        public EmployeeName name { get; set; }

        public int religion { get; set; }

        public string religionString { get; set; }

        public string nationality { get; set; }

        public int gender { get; set; }

        public string genderString { get; set; }

        public int idRef { get; set; }

        public DateTime? idExpiry { get; set; }

        public string idExpiryString { get; set; }

        public int passportRef { get; set; }

        public DateTime? passportExpiry { get; set; }

        public string passportExpiryString { get; set; }

        public string positionName { get; set; }

        public string branchName { get; set; }

        public string departmentName { get; set; }
        public string divisionName { get; set; }
        public string ehStatus { get; set; }
        public DateTime hireDate { get; set; }
        public string hireDateString { get; set; }
        public DateTime lastLeaveReturn { get; set; }
        public string lastLeaveReturnString { get; set; }

        public string hireLength { get; set; }

        public DateTime hireEndDate { get; set; }
        public string hireEndDateString { get; set; }

        public double taxableSalary { get; set; }
        public double unTaxableSalary { get; set; }

        public double finalSalary { get; set; }
        public bool isInactive { get; set; }

        public string isInactiveString { get; set; }

        public string terminationReason { get; set; }


    }
}

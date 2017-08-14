using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80302", "80")]
    public class RT302
    {
        public EmployeeName name { get; set; }
        public int caHours { get; set; }
        public int workingHours { get; set; }
        public int caDays { get; set; }
        public int workingDays { get; set; }

        public int OL_A { get; set; }
        public int OL_B { get; set; }
        public int OL_D { get; set; }

        public int overtime { get; set; }
        public int lateness { get; set; }


        public int lrPaid { get; set; }
        public int lrPaidCount { get; set; }
        public int lrUnpaid { get; set; }
        public int lrUnpaidCount { get; set; }

        public int netVariation { get; set; }
        public int absentDays { get; set; }

        public int missedPunches { get; set; }

        public string departmentName { get; set; }

        public string branchName { get; set; }
    }
}

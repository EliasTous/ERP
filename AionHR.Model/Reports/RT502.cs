using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80502","80")]
 public  class RT502
    {
        public EmployeeName name { get; set; }
        public string branchName { get; set; }
        public string departmentName { get; set; }
        public int overtime { get; set; }
        public int lateness { get; set; }
        public int absence { get; set; }
        public int disappearance { get; set; }
        public int missedPunches { get; set; }
        
        public decimal cvOvertime { get; set; }
        public decimal cvLateness { get; set; }
        public decimal cvAbsence { get; set; }
        public decimal cvDisappearance { get; set; }
        public decimal cvMissedPunches { get; set; }



    }
}

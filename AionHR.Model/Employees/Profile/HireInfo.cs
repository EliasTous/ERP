using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31141","31")]
   public class HireInfo

    {

        public string employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        [PropertyID("31141_01")]
        [ApplySecurity]
        public string npName { get; set; }
        [PropertyID("31141_02")]
        [ApplySecurity]
        public DateTime probationEndDate { get; set; }
        [PropertyID("31141_03")]
        [ApplySecurity]
        public DateTime nextReviewDate { get; set; }
        [PropertyID("31141_01")]
        [ApplySecurity]
        public int npId { get; set; }
        [PropertyID("31141_04")]
        [ApplySecurity]
        public DateTime termEndDate { get; set; }
        [PropertyID("31141_05")]
        [ApplySecurity]
        public string recruitmentInfo { get; set; }
        [PropertyID("31141_06")]
        [ApplySecurity]
        public string recruitmentCost { get; set; }
        public string pyReference { set; get;}
        public string taReference { set; get; }
        public DateTime pyActiveDate { set; get; }

        public int regBranchId { set; get; }






    }
}

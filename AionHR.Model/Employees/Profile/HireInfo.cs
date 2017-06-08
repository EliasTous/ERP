using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
   public class HireInfo

    {

        public string employeeId { get; set; }
        [PropertyID("31061_01")]
        [ApplySecurity]
        public string npName { get; set; }
        [PropertyID("31061_01")]
        [ApplySecurity]
        public DateTime probationEndDate { get; set; }
        [PropertyID("31061_01")]
        [ApplySecurity]
        public DateTime nextReviewDate { get; set; }
        public int npId { get; set; }
        [PropertyID("31061_01")]
        [ApplySecurity]
        public DateTime termEndDate { get; set; }
        [PropertyID("31061_01")]
        [ApplySecurity]
        public string recruitmentInfo { get; set; }
        [PropertyID("31061_01")]
        [ApplySecurity]
        public string recruitmentCost { get; set; }
        
    }
}

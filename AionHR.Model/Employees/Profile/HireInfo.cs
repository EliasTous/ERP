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

        public string npName { get; set; }
        public DateTime probationEndDate { get; set; }
        public DateTime nextReviewDate { get; set; }
        public int npId { get; set; }
        public DateTime termEndDate { get; set; }

        public string recruitmentInfo { get; set; }
        public string recruitmentCost { get; set; }
        
    }
}

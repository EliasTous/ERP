using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
    public class ProbationEnd
    {

        public EmployeeName employeeName { get; set; }

        public string npName { get; set; }

        public DateTime probationEndDate { get; set; }

        public DateTime nextReviewDate { get; set; }

        public int npId { get; set; }

        public DateTime termEndDate { get; set; }

        public int days { get; set; }
    }
}

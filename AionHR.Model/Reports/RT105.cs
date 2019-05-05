using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80105", "80")]
    public class RT105
    {
        public string name
        {
            get; set;
        }

        public DateTime date
        {
            set;
            get;
        }

        public string departmentName { get; set; }
        public string branchName { get; set; }
        public string positionName { get; set; }
        public string divisionName { get; set; }

        public string notes { get; set; }

        public EmployeeName reportToName
        {
            get; set;
        }
        public String DateString { get; set; }
    }
}

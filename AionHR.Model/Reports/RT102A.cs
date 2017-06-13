using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80102", "80")]
    public class RT102A
    {
        public EmployeeName name
        {
            get; set;
        }
        public string departmentName { get; set; }
        public string branchName { get; set; }
        public string positionName { get; set; }
        public string divisionName { get; set; }

        public DateTime date { get; set; }

        public string esName { get; set; }
        public string DateString { get; set; }
    }
}

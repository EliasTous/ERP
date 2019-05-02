using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80112", "80")]
    public class RT112
    {
        public string employeeName { get; set; }

        public string departmentName { get; set; }
        public string branchName { get; set; }

        public string userName { get; set; }
        public DateTime date { get; set; }
        public string dateString { get; set; }
        public string note { get; set; }

       
    }
}

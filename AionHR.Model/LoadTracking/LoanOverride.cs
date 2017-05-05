using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LoadTracking
{
    public class LoanOverride
    {

        public EmployeeName name { get; set; }

        public string employeeId { get; set; }

        public string ldMethod { get; set; }

        public string ldValue { get; set; }
    }
}

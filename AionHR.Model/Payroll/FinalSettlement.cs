using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    public class FinalSettlement:ModelBase
    {
        public EmployeeName name { get; set; }
        public string fsRef { get; set; }
        public DateTime date { get; set; }
        public string dateStringFormat { get; set; }
        public int employeeId { get; set; }

    }
}

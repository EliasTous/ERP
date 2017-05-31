using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeNote:ModelBase
    {

        public string employeeId { get; set; }

        public string employeeRef { get; set; }

        public string note { get; set; }

        public DateTime date { get; set; }
        
        public string userId { get; set; }

        public string userName { get; set; }
    }
}

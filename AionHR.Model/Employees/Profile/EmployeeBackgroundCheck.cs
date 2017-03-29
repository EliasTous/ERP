using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeBackgroundCheck:ModelBase
    {
        public int employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public int ctId { get; set; }

        public string ctName { get; set; }

        public string remarks { get; set; }
        public DateTime date { get; set; }
        public DateTime expiryDate { get; set; }

        public string fileUrl { get; set; }
    }
}

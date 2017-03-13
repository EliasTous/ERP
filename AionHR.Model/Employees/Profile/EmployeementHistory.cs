using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeementHistory :ModelBase
    {
        public int employeeId { get; set; }

        public EmployeeName employeeName { get; set; }

        public int statusId { get; set; }

        public string statusName { get; set; }

        public DateTime date { get; set; }

        public string comment { get; set; }
    }
}

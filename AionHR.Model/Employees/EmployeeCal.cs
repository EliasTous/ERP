using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees
{
   public class EmployeeCal
    {
        public EmployeeName employeeName { get; set; }
        public string caName { get; set; }
        public string scName { get; set; }
        public int? employeeId { get; set; }
        public string dayId { set; get; }

        public int? caId { get; set; }
        public int? scId { get; set; }

        public DateTime dayIdDt { get; set; }

    }
}

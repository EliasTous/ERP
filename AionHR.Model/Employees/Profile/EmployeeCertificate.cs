using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
   public class EmployeeCertificate:ModelBase
    {
        public int employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public int clId { get; set; }

        public string clName { get; set; }
        public string institution { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public double? grade { get; set; }

        public string major { get; set; }
    }
}

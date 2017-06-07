using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31041", "31")]
    public class EmployeeCertificate:ModelBase
    {
        public string institution { get; set; }
        public int clId { get; set; }

       
       
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

      
        public double? grade { get; set; }

        public string major { get; set; }

        public int employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public string clName { get; set; }
    }
}

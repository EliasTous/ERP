using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    
      [ClassIdentifier("80114", "80")]
    public class RT114 : ModelBase
    {
        public EmployeeName employeeName { get; set; }
        public string clName { get; set; }
        public int employeeId { get; set; }
        public int clId { get; set; }
        public string institution { get; set; }
        public string major { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public double grade { get; set; }
    }
}

using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.HelpFunction
{
   public class PayrollExport
    {
        public string payRef { get; set; }
        public EmployeeName employeeName { get; set; }
        public string currencyRef { get; set; }
        public int unitAmount { get; set; }
        public double oAmount { get; set; }
        public string description { get; set; }

        public string PayCodeRef  { get; set; }
    }
}

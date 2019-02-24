using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80504", "80")]
    public  class RT504
    {
        public EmployeeName name { get; set; }
        public double basicAmount { get; set; }
        public double eAmountTaxable { get; set; }
        public double eAmountNonTaxable { get; set; }
        public double dAmount { get; set; }
        public double netSalary { get; set; }

        public string idRef { get; set; }
        public string accountNumber { get; set; }
        public string swiftCode { get; set; }
       
       public DateTime startDate { get; set;  }
        public DateTime endDate { get; set; }
        






    }
}

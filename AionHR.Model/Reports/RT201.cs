using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
   public class RT201
    {
        public EmployeeName name
        {
            get; set;
        }

        public DateTime effectiveDate { get; set; }

        public int salaryType { get; set; }

        public int paymentFrequency { get; set; }

        public double basicAmount
        {
            get; set;
        }
        public string currencyRef { get; set; }
    }
}

using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
   public class SalaryChange
    {
        public EmployeeName employeeName { get; set; }

        public DateTime effectiveDate { get; set; }

        public double basicAmount { get; set; }

        public double eAmount { get; set; }

        public double dAmount { get; set; }

        public double finalAmount { get; set; }

        public string scrName { get; set; }

        public string currencyRef { get; set; }

        public int days { get; set; }
    }
}

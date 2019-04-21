using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
    [ClassIdentifier("81102", "81")]
    public class SalaryChange
    {
        public string employeeName { get; set; }

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

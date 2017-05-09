using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeSalary:ModelBase
    {
        public int employeeId { get; set; }
        public int currencyId { get; set; }

        public string currencyRef { get; set; }
        public int scrId { get; set; }

        public string scrName { get; set; }
        public DateTime effectiveDate { get; set; }
        public short salaryType { get; set; }
        public short paymentFrequency { get; set; }
        public short paymentMethod { get; set; }
        public short? isTaxable { get; set; }

        
        public string bankName { get; set; }
        public string accountNumber { get; set; }
        public string comments { get; set; }
        public double basicAmount { get; set; }
        public double finalAmount { get; set; }

        public double eAmount { get; set; }

        public double dAmount { get; set; }
    }
}

using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31062", "31")]
    public class EmployeeSalary:ModelBase
    {
        [PropertyID("31062_01")]
        [ApplySecurity]
        public int currencyId { get; set; }
        [PropertyID("31062_02")]
        [ApplySecurity]

        public int? scrId { get; set; }

        [PropertyID("31062_03")]
        [ApplySecurity]
        public DateTime effectiveDate { get; set; }
        [PropertyID("31062_04")]
        [ApplySecurity]
        public short salaryType { get; set; }
        [PropertyID("31062_05")]
        [ApplySecurity]
        public short paymentFrequency { get; set; }
        [PropertyID("31062_06")]
        [ApplySecurity]
        public short paymentMethod { get; set; }


        [PropertyID("31062_07")]
        [ApplySecurity]
        public string bankName { get; set; }
        [PropertyID("31062_08")]
        [ApplySecurity]
        public string accountNumber { get; set; }
        [PropertyID("31062_09")]
        [ApplySecurity]
        public string comments { get; set; }
        [PropertyID("31062_10")]
        [ApplySecurity]
        public double basicAmount { get; set; }
        [PropertyID("31062_11")]
        [ApplySecurity]
        public double finalAmount { get; set; }
        [PropertyID("31062_12")]
        [ApplySecurity]
        public double eAmount { get; set; }
        [PropertyID("31062_13")]
        [ApplySecurity]
        public double dAmount { get; set; }
        public int employeeId { get; set; }

        public EmployeeName employeeName { get; set; }
        [PropertyID("31062_02")]
        [ApplySecurity]
        public string scrName { get; set; }
        [PropertyID("31062_01")]
        [ApplySecurity]
        public string currencyRef { get; set; }
        public short? isTaxable { get; set; }
        public string EmpRef { set; get; }
    }
}

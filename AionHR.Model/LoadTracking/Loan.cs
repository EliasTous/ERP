using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LoadTracking
{
    [ClassIdentifier("45010", "45")]
    public class Loan:ModelBase,IEntity
    {
        [PropertyID("45010_01")]
        [ApplySecurity]
        public string employeeId { get; set; }


        [PropertyID("45010_02")]
        [ApplySecurity]
        public string loanRef { get; set; }
        [PropertyID("45010_03")]
        [ApplySecurity]
        public string branchId { get; set; }
        [PropertyID("45010_04")]
        [ApplySecurity]
        public int? ltId { get; set; }
        [PropertyID("45010_05")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("45010_06")]
        [ApplySecurity]
        public int currencyId { get; set; }
        [PropertyID("45010_07")]
        [ApplySecurity]
        public double amount { get; set; }
        [PropertyID("45010_08")]
        [ApplySecurity]
        public string purpose { get; set; }
        [PropertyID("45010_09")]
        [ApplySecurity]
        public short status { get; set; }


        [PropertyID("45010_10")]
        [ApplySecurity]

        public DateTime? effectiveDate { get; set; }
        [PropertyID("45010_01")]
        [ApplySecurity]
        public string employeeRef { get; set; }

        [PropertyID("45010_03")]
        [ApplySecurity]
        public string branchName { get; set; }
        [PropertyID("45010_01")]
        [ApplySecurity]
        // get
        public EmployeeName employeeName { get; set; }
        [PropertyID("45010_04")]
        [ApplySecurity]
        public string ltName { get; set; }
        [PropertyID("45010_06")]
        [ApplySecurity]
        public string currencyRef { get; set; }
        [PropertyID("45010_11")]
        [ApplySecurity]
        public double deductedAmount { get; set; }
    }
}

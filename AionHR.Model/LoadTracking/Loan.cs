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
    [ClassIdentifier("45050", "45")]
    public class Loan:ModelBase,IEntity
    {
        [PropertyID("45050_01")]
        [ApplySecurity]
        public string employeeId { get; set; }


        [PropertyID("45050_02")]
        [ApplySecurity]
        public string loanRef { get; set; }
        [PropertyID("45050_03")]
        [ApplySecurity]
        public string branchId { get; set; }
        [PropertyID("45050_04")]
        [ApplySecurity]
        public int? ltId { get; set; }
        [PropertyID("45050_05")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("45050_06")]
        [ApplySecurity]
        public int currencyId { get; set; }
        [PropertyID("45050_07")]
        [ApplySecurity]
        public double amount { get; set; }
        [PropertyID("45050_08")]
        [ApplySecurity]
        public string purpose { get; set; }
        [PropertyID("45050_09")]
        [ApplySecurity]
        public short status { get; set; }


        [PropertyID("45050_10")]
        [ApplySecurity]

        public DateTime? effectiveDate { get; set; }
        [PropertyID("45050_01")]
        [ApplySecurity]
        public string employeeRef { get; set; }

        [PropertyID("45050_03")]
        [ApplySecurity]
        public string branchName { get; set; }
        [PropertyID("45050_01")]
        [ApplySecurity]
        // get
        public EmployeeName employeeName { get; set; }
        [PropertyID("45050_04")]
        [ApplySecurity]
        public string ltName { get; set; }
        [PropertyID("45050_06")]
        [ApplySecurity]
        public string currencyRef { get; set; }
        [PropertyID("45050_11")]
        [ApplySecurity]
        public double deductedAmount { get; set; }
        [PropertyID("45050_12")]
        [ApplySecurity]
        public short ldMethod{ get; set; }
        [PropertyID("45050_13")]
        [ApplySecurity]
        public string ldValue { get; set; }
        public string statusString { get; set; }

        

    }
}

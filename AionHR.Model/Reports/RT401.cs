using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80401", "80")]
    public class RT401:ModelBase
    {
        public string employeeName { get; set; }

        public string currencyRef { get; set; }

        public DateTime? date { get; set; }
        public double deductedAmount { get; set; }
        public DateTime? effectiveDate { get; set; }
        public string ltName { get; set; }
        public string workingBranchName { get; set; }
        public string branchName { get; set; }
        public double amount { get; set; }
        public int status { get; set; }

        public string StatusString { get; set; }
        public string purpose { get; set; }
        public string deductedAmountString
        {
            get { return deductedAmount.ToString("N2") + " " + currencyRef; }
        }
        public string amountString
        {
            get { return amount.ToString("N2") + " " + currencyRef; }
        }
        public string NetString
        {
            get { return (amount- deductedAmount).ToString("N2") + " " + currencyRef; }
        }

        

    }
}

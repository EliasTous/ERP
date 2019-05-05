using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80202", "80")]
    public class RT202
    {
        public string name
        {
            get; set;
        }

        public string departmentName { get; set; }

        public string positionName { get; set; }
        public DateTime effectiveDate { get; set; }

        public int? salaryType { get; set; }

        public string SalaryTypeString
        {
            get
            {

                return salaryTypeString;

            }
            set
            {
                salaryTypeString = value;
            }
        }
        private string salaryTypeString;

        private string paymentFrequencyString;
        public int paymentFrequency { get; set; }
        public string PaymentFrequencyString
        {
            get
            {

                return paymentFrequencyString;

            }
            set
            {
                paymentFrequencyString = value;
            }
        }
        public double basicAmount
        {
            get; set;
        }
        public string currencyRef { get; set; }

        public string prevCurrencyRef { get; set; }

        public double? prevBasicAmount { get; set; }

        public int? prevSalaryType { get; set; }

        private string prevSalaryTypeString;
        public string PrevSalaryTypeString
        {
            get
            {

                return prevSalaryTypeString;

            }

            set
            {
                prevSalaryTypeString = value;
            }
        }

        public String DateString { get; set; }
    }
}

using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80201", "80")]
    public class RT201
    {
        public EmployeeName name
        {
            get; set;
        }

        public DateTime effectiveDate { get; set; }

        public int? salaryType { get; set; }

        private string salaryTypeString;
      
        public int? paymentFrequency { get; set; }

        private string paymentFrequencyString;

        public string PaymentFrequencyString
        {
            get
            {

                return paymentFrequencyString;

            }
            set { paymentFrequencyString = value; }
        }

        public double basicAmount
        {
            get; set;
        }
        public string currencyRef { get; set; }

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

        public String EffectiveDateString { get; set; }
        public double eAmount { get; set; }
        public double dAmount { get; set; }
        public double finalAmount { get; set; }
    }
}

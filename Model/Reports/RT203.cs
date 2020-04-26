using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80203", "80")]
    public class RT203
    {
        public string employeeName
        {
            get; set;
        }



        public string departmentName { get; set; }

        public string positionName { get; set; }
        public int? salaryType { get; set; }

   
        public double basicAmount
        {
            get; set;
        }
        private string salaryTypeString;
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
        public string currencyRef { get; set; }
        public double eAmount
        {
            get; set;
        }
        public double dAmount
        {
            get; set;
        }
        public double finalAmount
        {
            get; set;
        }
    }
}

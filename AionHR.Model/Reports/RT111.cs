using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80111", "80")]
    public  class RT111
    {
        public string employeeRef { get; set; }
        public string employeeName { get; set; }
        public string positionName { get; set; }

        public string departmentName { get; set; }
        public string branchName { get; set; }

        public string currencyRef { get; set; }
        public string accountNumber { get; set; }
        public string bankName { get; set; }
        public string swiftCode { get; set; }
        public double netSalary { get; set; }


    }
}

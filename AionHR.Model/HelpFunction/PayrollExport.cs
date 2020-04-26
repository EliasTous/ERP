using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HelpFunction
{
    [ClassIdentifier("85101", "85")]
    public class PayrollExport
    {
        public string payRef { get; set; }
        public string employeeName { get; set; }
        public string currencyRef { get; set; }
        public int unitAmount { get; set; }
        public double oAmount { get; set; }
        public string description { get; set; }

        public string PayCodeRef  { get; set; }
    }
}

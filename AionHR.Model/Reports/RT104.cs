using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80104", "80")]
    public class RT104
    {
        public EmployeeName name
        {
            get; set;
        }

        public int hiredMonth { get; set; }
        public string HiredMonthString { get;
            set; }
        public int hiredDay { get; set; }
         public int yearsInService { get; set; }


    }
}

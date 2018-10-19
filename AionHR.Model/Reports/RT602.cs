using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80602", "80")]
    public class RT602
    {
        public EmployeeName employeeName { get; set; }

        public double beginningBalance { get; set; }

        public double accrued { get; set; }

        public int used { get; set; }

        

        public int scheduled { get; set; }

        public double endingBalance { get; set; }
    }
}

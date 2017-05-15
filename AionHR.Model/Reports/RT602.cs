using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
   public class RT602
    {
        public EmployeeName employeeName { get; set; }

        public int beginningBalance { get; set; }

        public int accrued { get; set; }

        public int used { get; set; }

        public int scheduled { get; set; }

        public int endingBalance { get; set; }
    }
}

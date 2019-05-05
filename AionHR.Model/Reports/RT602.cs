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
        public string employeeId { get; set; }
        public DateTime? hireDate { get; set; }
        public string hireDateString { get; set; }
        public DateTime? lastReturnDate { get; set; }
        public string lastReturnDateString { get; set; }
        public string employeeName { get; set; }
        public double salary { get; set; }
       

        public double accrued { get; set; }

        public double used { get; set; }

        public double paid { get; set; }
        public double balance { get; set; }


    }
}

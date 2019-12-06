using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
    [ClassIdentifier("81103", "81")]
    public class WorkAnniversary
    {
        public EmployeeName name { get; set; }

        public DateTime birthDate
        {
            get; set;
        }
        public DateTime hireDate
        {
            get; set;
        }


        public int days { get; set; }
        public string birthDateString { get; set; }
        public string hireDateString { get; set; }
    }
}

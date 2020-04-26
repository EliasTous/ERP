using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
    [ClassIdentifier("81104", "81")]
    public class EmployeeBirthday
    {

        public EmployeeName name { get; set; }

        public DateTime birthDate
        {
            get; set;
        }
        public string birthDateString
        {
            get; set;
        }

        public int days { get; set; }
    }
}

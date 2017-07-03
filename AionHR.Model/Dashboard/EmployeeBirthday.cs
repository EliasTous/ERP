using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
    public class EmployeeBirthday
    {

        public EmployeeName employeeName { get; set; }

        public DateTime birthDate
        {
            get; set;
        }

        public int days { get; set; }
    }
}

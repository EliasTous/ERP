using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
   public class RT203
    {
        public EmployeeName name
        {
            get; set;
        }



        public string departmentName { get; set; }

        public string positionName { get; set; }
        public int salaryType { get; set; }

   
        public double basicAmount
        {
            get; set;
        }
        public string currencyRef { get; set; }
    }
}

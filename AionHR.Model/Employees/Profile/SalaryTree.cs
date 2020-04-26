using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
   public class SalaryTree
    {
        public List<SalaryDetail> Details { get; set; }
        public EmployeeSalary Basic { get; set; }
    }
}

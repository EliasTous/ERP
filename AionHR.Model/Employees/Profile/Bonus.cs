using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
   public class Bonus:ModelBase
    {
        public int employeeId { get; set; }

        public EmployeeName employeeName { get; set; }

        public int btId { get; set; }

        public string btName { get; set; }

        public string currencyName { get; set; }

        public DateTime date { get; set; }

        public int amount { get; set; }

        public int currencyId { get; set; }

        public string comment { get; set; }
    }
}

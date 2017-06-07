using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31031", "31")]
    public class Bonus:ModelBase
    {
        public int currencyId { get; set; }

        
       

        

        public int btId { get; set; }

        

        

        public DateTime date { get; set; }

        public int amount { get; set; }

      
        public string comment { get; set; }
        public string currencyRef { get; set; }
        public int employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public string btName { get; set; }
    }
}

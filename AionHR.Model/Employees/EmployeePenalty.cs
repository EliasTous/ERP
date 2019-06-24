using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees
{
    [ClassIdentifier("31170", "31")]
    public  class EmployeePenalty:ModelBase
    {
        public string employeeName { get; set; }
       // public string employeeName { get; set; }
        public string penaltyName { get; set; }
        public string employeeId { get; set; }
        public string penaltyId { get; set; }
        public DateTime date  { get; set; }
        public string apStatus { get; set; }
        public string notes { get; set; }
        public double amount { get; set; }



    }
}

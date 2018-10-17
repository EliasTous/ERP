using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees
{
  public  class EmployeePenalty:ModelBase
    {
        public EmployeeName employeeName { get; set; }
        public string penaltyName { get; set; }
        public string employeeId { get; set; }
        public string penaltyId { get; set; }
        public DateTime Date  { get; set; }
        public string apStatus { get; set; }
        public string notes { get; set; }



    }
}

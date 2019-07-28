using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees
{
 public   class EmployeeUserValue
    {
        public string propertyName { get; set; }

        public short mask { get; set; }
        public string maskString { get; set; }
        public string propertyId { get; set; }
        public string employeeId { get; set; }
        public string value { get; set; }
    }
}

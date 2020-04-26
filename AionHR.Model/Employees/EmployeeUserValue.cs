using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees
{
 public   class EmployeeUserValue
    {
        public string propertyName { get; set; }

        public short mask { get; set; }
        public string maskString { get; set; }
        public string propertyId { get; set; }
        public string employeeId { get; set; }
        public string value { get; set; }


        public double GetValueDouble()
        { return Convert.ToDouble(value); }
        public DateTime GetValueDateTime()
        { return DateTime.Parse(value); }

        public bool GetValueBool()
        {
            return value == "true";
        }
    }
}

using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.SelfService
{
    [ClassIdentifier("60109", "60")]
    public  class FlatScheduleSelfService
    {
        public string dayId { get; set; }

        public int shiftId { get; set; }

        public string from { get; set; }

        public string to { get; set; }
        public int departmentId { get; set; }
        public string departmentName { get; set; }

        public int employeeId { get; set; }

        public string fromDayId { get; set; }
        public string toDayId { get; set; }
        public string duration { get; set; }
        public EmployeeName employeeName { get; set; }
    }
}

using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
   public class OvertimeSetting
    {
        public int employeeId { get; set; }

        public EmployeeName employeeName { get; set; }
        public string dayId { get; set; }

        public int maxOvertime { get; set; }
    }
}

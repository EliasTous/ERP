using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    [ClassIdentifier("41080","41")]
   public class OvertimeSetting
    {
        [PropertyID("4108001")]
        public int employeeId { get; set; }
        [PropertyID("4108001")]
        public EmployeeName employeeName { get; set; }
        [PropertyID("4108002")]
        public string dayId { get; set; }
        [PropertyID("4108003")]
        public int maxOvertime { get; set; }
        [PropertyID("4108004")]
        public int minOvertime { get; set; }

        public string reference { get; set; }
    }
}

using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TimeAttendance
{
    [ClassIdentifier("41063", "41")]
   public class OvertimeSetting
    {
        [PropertyID("4108001")]
        public int employeeId { get; set; }
        [PropertyID("4108001")]
        public string employeeName { get; set; }
        [PropertyID("4108002")]
        public string dayId { get; set; }
        [PropertyID("4108003")]
        public int maxOvertime { get; set; }
        [PropertyID("4108004")]
        public int minOvertime { get; set; }

        public string reference { get; set; }
    }
}

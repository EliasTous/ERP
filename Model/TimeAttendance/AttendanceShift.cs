using Model.Attributes;
using Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TimeAttendance
{
    [ClassIdentifier("41061", "41")]
    public class AttendanceShift
    {
        [PropertyID("41061_01")]
        [ApplySecurity]
        public string checkIn { get; set; }
        [PropertyID("41061_02")]
        [ApplySecurity]
        public string checkOut { get; set; }
        public string dayId { get; set; }

        public string recordId { get; set; }

        public string employeeRef { get; set; }
        public string employeeId { get; set; }

        [PropertyID("41061_03")]
        [ApplySecurity]
        public string duration { get; set; }
    }
}

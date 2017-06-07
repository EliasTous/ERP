using AionHR.Model.Attributes;
using AionHR.Model.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    [ClassIdentifier("41061", "41")]
    public class AttendanceShift
    {

        public string checkIn { get; set; }
        public string checkOut { get; set; }
        public string dayId { get; set; }

        public string recordId { get; set; }

        public string employeeRef { get; set; }
        public string employeeId { get; set; }

       
        public string duration { get; set; }
    }
}

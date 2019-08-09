using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
 public   class UnschedulePunch
    {
        public string employeeId { get; set; }
        public string employeeRef { get; set; }
        public string employeeName { get; set; }
        public string AttendedHours { get; set; }
        public string variation { get; set; }
    }
}

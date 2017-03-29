using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class AttendanceShift
    {
        public string dayId { get; set; }

        public int employeeId { get; set; }

        public string checkIn { get; set; }
        public string checkOut { get; set; }
        public string duration { get; set; }
    }
}

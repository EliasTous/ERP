using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TimeAttendance
{
    [ClassIdentifier("84102", "84")]
    public class LeaveCalendarDay
    {
        public int caId { get; set; }
        public int year { get; set; }
        public string dayId { get; set; }
        public int scId { get; set; }
        public int dayTypeId { get; set; }
        public short dow { get; set; }

        public bool isWorkingDay { get; set; }

        public double workingHours { get; set; }
    }
}

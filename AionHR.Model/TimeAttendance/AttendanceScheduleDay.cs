using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41051", "41")]
    public class AttendanceScheduleDay
    {
        [PropertyID("41051_01")]
        [ApplySecurity]
        public string dayTypeId { get; set; }
        [PropertyID("41051_02")]
        [ApplySecurity]
        public string firstIn { get; set; }
        [PropertyID("41051_03")]
        [ApplySecurity]
        public string lastOut { get; set; }
        public int scId { get; set; }
        public short dow { get; set; }



        [PropertyID("41051_04")]
        [ApplySecurity]
        public string duration { get; set; }

        public string durationFormatted
        {
            get
            {
                int hours;
                int mins;
                int durationMins = Convert.ToInt32(duration);
                hours = durationMins / 60;
                mins = durationMins % 60;
                return string.Format(hours.ToString().PadLeft(2, '0') + ":" + mins.ToString().PadLeft(2, '0'));
            }
        }
    }
}

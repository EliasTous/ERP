using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41072", "41")]
    public class CalendarDay
    {
        public int scId { get; set; }
        public int dayTypeId { get; set; }
        public int caId { get; set; }
        public int year { get; set; }
        public string dayId { get; set; }
        
        
        public short dow { get; set; }
    }
}

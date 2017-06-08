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
        [PropertyID("41072_01")]
        [ApplySecurity]
        public int scId { get; set; }
        [PropertyID("41072_02")]
        [ApplySecurity]
        public int dayTypeId { get; set; }
        public int caId { get; set; }
        public int year { get; set; }
        public string dayId { get; set; }
        
        
        public short dow { get; set; }
    }
}

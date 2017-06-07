using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41071", "41")]
    public class CalendarYear
    {
        public int caId { get; set; }
        public int year { get; set; }
    }
}

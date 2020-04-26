using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Attendance
{
    [ClassIdentifier("41071", "41")]
    public class CalendarYear
    {
       
        public int caId { get; set; }
        [PropertyID("41071_01")]
        [ApplySecurity]
        public int year { get; set; }
    }
}

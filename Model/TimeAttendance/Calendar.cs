using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Attendance
{
    [ClassIdentifier("41070", "41")]
    public class WorkingCalendar:ModelBase
    {
        [PropertyID("41070_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}

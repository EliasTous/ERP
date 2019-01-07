using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.HelpFunction
{
    [ClassIdentifier("84107", "84")]
    public  class SynchronizeAttendanceDay
    {
        public int employeeId { get; set; }
        public string fromDayId { get; set; }
        public string toDayId { get; set; }
    }
}

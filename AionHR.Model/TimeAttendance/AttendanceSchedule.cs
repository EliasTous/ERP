using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41050", "41")]
    public class AttendanceSchedule:ModelBase
    {
        public string name { get; set; }
        public short fci_min_ot { get; set; }
        public short fci_max_lt { get; set; }
        public short lco_max_el { get; set; }
        public short lco_min_ot { get; set; }
        public short lco_max_ot { get; set; }
    }
}

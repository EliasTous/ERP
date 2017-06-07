using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41052", "41")]
    public class AttendanceBreak
    {
        
        public string name { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool? isBenefitOT { get; set; }

        public int scId { get; set; }
        public short dow { get; set; }
        public short seqNo { get; set; }
    }
}

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
        [PropertyID("41052_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("41052_02")]
        [ApplySecurity]
        public string start { get; set; }
        [PropertyID("41052_03")]
        [ApplySecurity]
        public string end { get; set; }
        [PropertyID("41052_04")]
        [ApplySecurity]
        public bool? isBenefitOT { get; set; }

        public int scId { get; set; }
        public short dow { get; set; }
        public short seqNo { get; set; }
    }
}

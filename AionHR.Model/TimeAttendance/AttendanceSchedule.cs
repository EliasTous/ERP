using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Attendance
{
    [ClassIdentifier("41050", "41")]
    public class AttendanceSchedule:ModelBase
    {
        [PropertyID("41050_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("41050_02")]
        [ApplySecurity]
        public short? fci_min_ot { get; set; }
        [PropertyID("41050_03")]
        [ApplySecurity]
        public short? fci_max_lt { get; set; }
        [PropertyID("41050_04")]
        [ApplySecurity]
        public short? lco_max_el { get; set; }
        [PropertyID("41050_05")]
        [ApplySecurity]
        public short? lco_min_ot { get; set; }
        [PropertyID("41050_05")]
        [ApplySecurity]
        public short? lco_max_ot { get; set; }

        [PropertyID("41050_02")]
        [ApplySecurity]
        public bool? enableAOT { get; set; }

        [PropertyID("41050_06")]
        [ApplySecurity]
        public bool? enableBOT { get; set; }

        [PropertyID("41050_05")]
        [ApplySecurity]
        public bool? enableDOT { get; set; }
        [PropertyID("41050_06")]
        [ApplySecurity]
        public short? b_min_ot { get; set; }
        [PropertyID("41050_06")]
        [ApplySecurity]
        public short? b_max_ot { get; set; }



    }
}

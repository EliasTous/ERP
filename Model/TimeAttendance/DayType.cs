using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Attendance
{
    [ClassIdentifier("41030", "41")]
    public class DayType:ModelBase
    {
        [PropertyID("41030_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("41030_02")]
        [ApplySecurity]
        public bool isWorkingDay { get; set; }
        [PropertyID("41030_03")]
        [ApplySecurity]
        public string color { get; set; }
    }
}

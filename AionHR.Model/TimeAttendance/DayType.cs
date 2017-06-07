using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41030", "41")]
    public class DayType:ModelBase
    {
        public string name { get; set; }
       
        public bool isWorkingDay { get; set; }
        public string color { get; set; }
    }
}

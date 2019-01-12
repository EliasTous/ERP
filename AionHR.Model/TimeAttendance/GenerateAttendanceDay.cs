using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
   
    public class GenerateAttendanceDay:ModelBase

    {
        public int employeeId { get; set; }
        public string fromDayId { get; set; }
        public string toDayId { get; set; }
        public int branchId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TimeAttendance
{
    public class SchedulePattern
    {
        public int scId { get; set; }
        public int fdow { get; set; }

        public string timeFrom { get; set; }
        public string timeTo { get; set; }
        public string breakFrom { get; set; }
        public string breakTo { get; set; }
         
        public int workingDays { get; set; }

        public int workingDayTypeId { get; set; }

        public int weekendDayTypeId { get; set; }
    }
}

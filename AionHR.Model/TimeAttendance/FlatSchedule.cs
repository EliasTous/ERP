using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class FlatSchedule
    {

     
        public string dayId { get; set; }

        public int  shiftId { get; set; }

        public string from { get; set; }
        
        public string to { get; set; }

   
        public int employeeId { get; set; }

        public string fromDayId { get; set; }
        public string toDayId { get; set; }


    }
    public class FlatScheduleRange : FlatSchedule
    {
    }
}

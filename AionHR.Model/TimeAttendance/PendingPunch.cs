using AionHR.Model.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
   public class PendingPunch : Check
    {
        public string recordId { get; set; }
        public short? ppType { get; set; }
        public string ppTypeString { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
   public class FlatPunch
    {
        public string punchId { get; set; }
        public string shiftId { get; set; }
        public DateTime clockStamp { get; set; }
    }
}

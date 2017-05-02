using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LeaveManagement
{
    public class LeaveDay:ModelBase
    {
        public int leaveId { get; set; }

        public string dayId { get; set; }

        public double workingHours { get; set; }

        public double leaveHours { get; set; }

        public short dow { get; set; }
    }

}

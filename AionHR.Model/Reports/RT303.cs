using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80303", "80")]
    public class RT303
    {
        public string employeeName { get; set; }
     
        public string dayId { get; set; }
       
        public string branchName { get; set; }
        public string effectiveTime { get; set; }

      
        public string firstPunch { get; set; }
        public string lastPunch { get; set; }
        public string duringShiftLeave { get; set; }
        public string earlyLeave { get; set; }
        public string lateCheckin { get; set; }
        public string shiftLeaveWithoutRequest { get; set; }
    }
}

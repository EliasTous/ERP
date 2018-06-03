using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LeaveManagement
{
    [ClassIdentifier("42021", "42")]
    public class LeaveDay:ModelBase
    {
        
        public int leaveId { get; set; }
        [PropertyID("42021_01")]
        [ApplySecurity]
        public string dayId { get; set; }
        [PropertyID("42021_02")]
        [ApplySecurity]
        public double workingHours { get; set; }
        [PropertyID("42021_03")]
        [ApplySecurity]
        public double leaveHours { get; set; }

        public short dow { get; set; }
        public int employeeId { set; get; }
    }

}

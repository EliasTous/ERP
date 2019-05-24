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
        public string employeeId { get; set; }
        public string dayId { get; set; }
        public short? dow { get; set; }
        public string departmentName { get; set; }
        public string branchName { get; set; }
        public bool onLeave { get; set; }
        public bool paidLeave { get; set; }
        public string dayStart { get; set; }
        public string dayEnd { get; set; }
        public string firstIn { get; set; }
        public string lastOut { get; set; }
        public bool UNPAID_LEAVE { get; set; }
        public bool PAID_LEAVE { get; set; }
        public bool DAY_LEAVE_WITHOUT_EXCUSE { get; set; }
        public bool SHIFT_LEAVE_WITHOUT_EXCUSE { get; set; }
        public bool MISSED_PUNCH { get; set; }
        public double? LATE_CHECKIN { get; set; }
        public double? DURING_SHIFT_LEAVE { get; set; }
        public double? EARLY_LEAVE { get; set; }
        public double? EARLY_CHECKIN { get; set; }
        public double? OVERTIME { get; set; }
        public double? scheduledHours { get; set; }
        public double? workingHours { get; set; }
    }
}

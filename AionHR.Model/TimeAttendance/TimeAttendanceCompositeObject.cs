using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class TimeAttendanceCompositeObject
    {
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }
        public string branchName { get; set; }
        public string dayId { get; set; }
        public string FSString { get; set; }
        public string ASString { get; set; }
        public string TVString { get; set; }
        public string dayIdString { get; set; }
        public string schedule { get; set; }

        public string attendance { get; set; }
        public string effectiveTime { get; set; }

    }
}

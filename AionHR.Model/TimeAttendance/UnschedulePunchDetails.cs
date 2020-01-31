using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class UnschedulePunchDetails : ModelBase
    {
        public string employeeId { get; set; }
        public string employeeRef { get; set; }
        public string employeeName { get; set; }
        public string positionName { get; set; }
        public string branchName { get; set; }

        public double ciPunchId { get; set; }
        public double coPunchId { get; set; }
        public string dayIdDateTime { get; set; }

        public string dayId { get; set; }
        public int shiftId { get; set; }
       
        public string checkIn { get; set; }
        public string checkOut { get; set; }
        public double duration { get; set; }

        public string strDuration { get; set; }

    }
}

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
       
        public string earlyCheckin { get; set; }
        public string overtime { get; set; }
        public string totalLateness  { get; set; }
        public string totalOvertime { get; set; }
        public string dayStatus { get; set; }


        public double totTotalLateness { get; set; }

        public double totTotalOvertime { get; set; }

        public string bTotTotalLateness { get; set; }

        public string bTotTotalOvertime { get; set; }

        public double lineTotalOvertime { get; set; }
        public double lineDuringShiftLeave { get; set; }
        public double lineEarlyCheckIn { get; set; }
        public double lineLateCheckIn { get; set; }
        public double lineEarlyLeave { get; set; }
        public double totEffectiveTime { get; set; }
        public double missedShift { get; set; }
        public string strMissedShift { get; set; }


        public int punchCount { get; set; }
        public double netLateness { get; set; }
        public double netOvertime { get; set; }


        public string departmentName { get; set; }

        public string positionName { get; set; }
        public string employeeRef { get; set; }

        public string strEffectiveTime { get; set; }

        public string ttEffTime { get; set; }

        public double NET_LATENESS { get; set; }
        public double NET_OVERTIME { get; set; }
        public int dayStatusId { get; set; }

        public bool DAY_LEAVE_WITHOUT_EXCUSE { get; set; }
        public double MISSED_PUNCH { get; set; }

        public double SHIFT_LEAVE_WITHOUT_EXCUSE { get; set; }
        public double LATE_CHECKIN { get; set; }

        public double DURING_SHIFT_LEAVE { get; set; }
        public double EARLY_LEAVE { get; set; }

        public double EARLY_CHECKIN { get; set; }
        public double OVERTIME { get; set; }

        public List<AionHR.Model.TimeAttendance.DetailedAttendanceVariation> variationsList { get; set; }



    }
}

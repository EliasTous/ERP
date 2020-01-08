using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using AionHR.Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41062", "41")]
    public class AttendanceDay
    {
        public string positionName;

        [PropertyID("41062_01")]
        [ApplySecurity]
        public string employeeName { get; set; }
        [PropertyID("41062_02")]
        [ApplySecurity]
        public int branchId { get; set; }
        [PropertyID("41062_03")]
        [ApplySecurity]
        public int departmentId { get; set; }
        [PropertyID("41062_03")]
        [ApplySecurity]
        public string departmentName { get; set; }
        [PropertyID("41062_04")]
        [ApplySecurity]
        public string checkIn { get; set; }
        [PropertyID("41062_05")]
        [ApplySecurity]
        public string checkOut { get; set; }
        [PropertyID("41062_06")]
        [ApplySecurity]
        public string workingTime { get; set; }
        [PropertyID("41062_07")]
        [ApplySecurity]
        public string breaks { get; set; }
        [PropertyID("41062_02")]
        [ApplySecurity]
        public string branchName { get; set; }
        [PropertyID("41062_08")]
        [ApplySecurity]
        public string dayId { get; set; }

        public string dayIdString{ get; set; }
        [PropertyID("41062_01")]
        [ApplySecurity]
        public int employeeId { get; set; }

       public string caName { get; set; }

        public string scName { get; set; }
        public short apStatus { get; set; }
        public string apStatusString { get; set; }
        public short duration { get; set; }

        public int netOL { get; set; }
        public string netOLString { get; set; }
        public string OL_A { get; set; }
        public string OL_B { get; set; }
        public string OL_D { get; set; }
        public string OL_N { get; set; }
        public int OL_A_SIGN { get; set; }
        public int OL_B_SIGN { get; set; }
        public int OL_D_SIGN { get; set; }
        public int OL_N_SIGN { get; set; }

        public string schedule { get; set; }

        public string attendance { get; set; }

        public string variation { get; set; }
        public string effectiveTime { get; set; }

        public List<DetailedAttendanceVariation> variationsList { get; set; }
        public string firstPunch { get; set; }
        public string lastPunch { get; set; }
        public string branchRef { get; set; }

        public string dayStatus { get; set; }
    }
}

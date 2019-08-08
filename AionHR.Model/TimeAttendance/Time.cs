using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    [ClassIdentifier("41065", "41")]
    public  class Time
    {
        public string employeeName { set; get; }
        public string seqNo { set; get; }
        public string approverName { set; get; }
        public string tvId { set; get; }
        public string fullName { get; set; }
        public string departmentName { get; set; }
        public string branchName { get; set; }
        public string dayId { set; get;  }
        public string dayIdString { set; get; }
        public DateTime? dayIdDate
        {
            get
            {
                if (String.IsNullOrEmpty(dayId))
                    return null; 
                return DateTime.ParseExact(dayId, "yyyyMMdd", new CultureInfo("en"));
            }
            set { value = dayIdDate; }
        }
        public int employeeId { get; set; }
        public string timeCode { set; get; }
        public string timeCodeString { set; get; }
        public string shiftId { set; get; }
        
        public int  approverId { get; set; }

        public short status { get; set; }
        public string notes { set; get; }

        public string statusString { get; set; }


        public string clockDuration { get; set; }
        public string clockDurationString { get; set; }
        public string duration { get; set; }
        public string durationString { get; set; }
        public string damageLevel { get; set; }
        public string shiftStart { get; set; }
        public string shiftEnd { get; set; }
        public string apId { get; set; }
        public string apName { get; set; }
        public string justification { get; set; }

        


    }
}

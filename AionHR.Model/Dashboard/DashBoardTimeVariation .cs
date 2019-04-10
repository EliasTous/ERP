using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
    [ClassIdentifier("41066", "41")]
    public  class DashBoardTimeVariation
    {
        public string recordId { get { return employeeId.ToString() + dayId.ToString() + shiftId.ToString() + timeCode.ToString(); } }
        public int employeeId { get; set; }
        public string dayId { get; set; }
        public DateTime dayIdDate { get; set; }
        public string dayIdString { get; set; }
        public short shiftId { get; set; }
        public short timeCode { get; set; }
        public string timeCodeString { get; set; }
        public short clockDuration { get; set; }
        public string clockDurationString { get; set; }
        public short? apStatus { get; set; }
        public string apStatusString { get; set; }
        public short duration { get; set; }
        public string durationString { get; set; }

        public EmployeeName employeeName { get; set; }
        public string branchName { get; set; }
        public string positionName { get; set; }
        public short? damageLevel { get; set; }
        public string damageLevelString { get; set; }
        public string justification { get; set; }


    }
}

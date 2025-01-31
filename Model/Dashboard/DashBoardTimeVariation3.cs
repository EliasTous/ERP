﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
    public class DashBoardTimeVariation3 : ModelBase
    {
        public string timeName;

        public string primaryKey { get { if (!string.IsNullOrEmpty(dayId)) { return employeeId.ToString() + dayId.ToString() + shiftId.ToString() + timeCode.ToString(); } else return employeeId.ToString() + shiftId.ToString() + timeCode.ToString(); } }
        public int employeeId { get; set; }
        public string dayId { get; set; }
        public DateTime dayIdDate { get; set; }
        public string dayIdString { get; set; }
        public int? shiftId { get; set; }
        public short timeCode { get; set; }
        public string timeCodeString { get; set; }
        public short clockDuration { get; set; }
        public string clockDurationString { get; set; }
        public short? apStatus { get; set; }
        public string apStatusString { get; set; }
        public short duration { get; set; }
        public string durationString { get; set; }

        public string employeeName { get; set; }
        public string branchName { get; set; }
        public string positionName { get; set; }
        public short? damageLevel { get; set; }
        public string damageLevelString { get; set; }
        public string justification { get; set; }
        public DateTime dtFrom { get; set; }
        public DateTime dtTo { get; set; }


        public string employeeRef { get; set; }
        public DateTime? date { get; set; }
        public string apId { get; set; }

        public string arId { get; set; }
        public string arString { get; set; }

    }
}

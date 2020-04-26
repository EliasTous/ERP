using Model.Attributes;
using Model.Employees.Profile;
using Model.TimeAttendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TimeAttendance
{
    public class MonthlyLateness
    {
        public int employeeId { get; set; }
        public string employeeRef { get; set; }
        public string employeeName { get; set; }

        public string departmentName { get; set; }

        public string branchName { get; set; }

        public string positionName { get; set; }

        public int clockDuration { get; set; }
        public int duration { get; set; }
        public int netLateness { get; set; }

        public string strClockDuration { get; set; }
        public string strDuration { get; set; }
        public string strNetLateness { get; set; }

    }
}

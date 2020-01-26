using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80302", "80")]
    public class RT302
    {
        //public string employeeId { get; set; }
        //public string employeeName { get; set; }

        public EmployeeName name { get; set; }
        public string departmentName { get; set; }

        public string branchName { get; set; }

        public double calendarHours { get; set; }
        public double workingHours { get; set; }
        public int calendarDays { get; set; }

        public int workingDays { get; set; }
        public double UNPAID_LEAVE { get; set; }

        public double Absent { get; set; }
        public double Total_Absent { get; set; }
        public double PAID_LEAVE { get; set; }
        public double DAY_LEAVE_WITHOUT_EXCUSE { get; set; }
        public double SHIFT_LEAVE_WITHOUT_EXCUSE { get; set; }
        public string SHIFT_LEAVE_WITHOUT_EXCUSE_String { get; set; }
        public double MISSED_PUNCH { get; set; }
        public double LATE_CHECKIN { get; set; }
        public double DURING_SHIFT_LEAVE { get; set; }
        public double EARLY_LEAVE { get; set; }
        public double EARLY_CHECKIN { get; set; }

        public double OVERTIME { get; set; }
        //public int caHours { get; set; }

        //public int caDays { get; set; }


        //public int OL_A { get; set; }
        //public int OL_B { get; set; }
        //public int OL_D { get; set; }

        //public short overtime { get; set; }
        //public double netLateness { get; set; }
        //public double grossLateness { get; set; }

        //public int lrPaid { get; set; }
        //public int lrPaidCount { get; set; }
        //public int lrUnpaid { get; set; }
        //public int lrUnpaidCount { get; set; }

        //public int netVariation { get; set; }
        //public int absentDaysWithRequest { get; set; }

        //public int absentDaysWithoutRequest { get; set; }

        //public int missedPunches { get; set; }

        public double totLateness { get; set; }
        public double totOvertime { get; set; }


    }
}

using AionHR.Model.Employees.Profile;
using System;

namespace AionHR.Model.Attendance
{
    public  class CheckMonitor 
    {
        public int figureId { get; set; }

        public string figureTitle { get; set; }
        public int count { get; set; }
        public double rate { get; set; }
    }
    public  class ActiveCheck : ModelBase
    {
        public int employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public short checkStatus { get; set; }
        public string time { get; set; }
        public string positionName { get; set; }
        public string departmentName { get; set; }
        public string branchName { get; set; }

    }

    public  class ActiveLate : ActiveCheck
    {
    }

    public  class ActiveOut : ActiveCheck
    {
    }

    public  class MissedPunch : ModelBase
    {
        public int employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public string dayId { get; set; }
        public bool missedIn { get; set; }
        public bool missedOut { get; set; }
        public string time { get; set; }

    }

    public  class ActiveAbsence : ModelBase
    {
        public int employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public string positionName { get; set; }
        public string departmentName { get; set; }
        public string branchName { get; set; }

    }

    public  class ActiveLeave : ModelBase
    {
        public int employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string ltName { get; set; }
        public string destination { get; set; }

    }
}
using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    [ClassIdentifier("84111", "84")]
    public class FlatSchedule
    {

        
        public string dayId { get; set; }

        public int shiftId { get; set; }

        public string from { get; set; }

        public string to { get; set; }
        public int departmentId { get; set; }
        public string departmentName { get; set; }
        
        public int employeeId { get; set; }
        public string employeeRef { get; set; }

        public string fromDayId { get; set; }
        public string toDayId { get; set; }
        public string duration { get; set; }
        public EmployeeName employeeName { get; set; }

    }
    [ClassIdentifier("84104", "84")]
    public class FlatBulkSchedule
    {
        public string fromDayId { get; set; }
        public string toDayId { get; set; }
        public int employeeId { get; set; }
        public string shiftStart { get; set; }

        public string shiftEnd { get; set; }
    }


    public class FlatScheduleRange : FlatSchedule
    {
    }
    [ClassIdentifier("84101", "84")]
    public class FlatScheduleBranchAvailability
    {
        public int cnt { get; set; }
        public string dayId { get; set; }
        public string time { get; set; }

    }
    [ClassIdentifier("84103", "84")]
    public class FlatScheduleImport
    {
        public int fromEmployeeId { get; set; }
        public int toEmployeeId { get; set; }
        public string fromDayId { get; set; }
        public string toDayId { get; set; }

    }
    [ClassIdentifier("84105", "84")]
    public class FlatScheduleEmployeeCell
    {
        public int employeeId { get; set; }

        public EmployeeName employeeName { get; set; }




    }
    [ClassIdentifier("84106", "84")]
    public class FlatScheduleWorkingHours
    {
      
        public double workingHours { get; set; }
    }


}

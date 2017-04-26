using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AionHR.Model.Reports
{
    public class EmployeeAttendances

    {
        public EmployeeName name { get; set; }
        public string branchName { get; set; }

        public string departmentName { get; set; }

        public string positionName { get; set; }
        public List<Attendance> attendances { get; set; }
    }

    public class Attendance
    {
        public string timeIn { get; set; }

        public string timeOut { get; set; }

        public int day { get; set; }

        public int month { get; set; }

    }
}
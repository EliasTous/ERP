using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    public class AttendanceDay
    {
        
        public EmployeeName employeeName { get; set; }
        public int branchId { get; set; }

        public int departmentId { get; set; }

        public string departmentName { get; set; }
        public string checkIn { get; set; }
        public string checkOut { get; set; }
        public string workingTime { get; set; }
        public string breaks { get; set; }
        public string branchName { get; set; }
        public string dayId { get; set; }
        public int employeeId { get; set; }

       
        
        
        public int netOL { get; set; }
        public string OL_A { get; set; }
        public string OL_B { get; set; }
        public string OL_D { get; set; }
        public string OL_N { get; set; }
        public int OL_A_SIGN { get; set; }
        public int OL_B_SIGN { get; set; }
        public int OL_D_SIGN { get; set; }
        public int OL_N_SIGN { get; set; }
    }
}

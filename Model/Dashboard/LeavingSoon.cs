using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
   public class LeavingSoon : ModelBase
    {
        public string employeeName { get; set; }
        public string replacementName { get; set; }
        public int apId { get; set; }
        public string ltName { get; set; }
        public string departmentName { get; set; }
        public string branchName { get; set; }
        public string employeeId { get; set; }
        public string leaveRef { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime? returnDate { get; set; }
        public string ltId { get; set; }
        public string replacementId { get; set; }
        public short status { get; set; }
        public double leaveDays { get; set; }
        public string destination { get; set; }
        public string justification { get; set; }
        public string returnNotes { get; set; }

    }
}

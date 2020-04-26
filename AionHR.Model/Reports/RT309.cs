using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
 public   class RT309
    {
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string dayId { get; set; }
        public List<ShiftLog> shiftLog { get; set; }
        public string shiftId { get; set; }
        public DateTime dayIdDateTime { get; set; }
        public int duration { get; set; }
        public string employeeRef { get; set; }
        public string branchName { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }

    }
}

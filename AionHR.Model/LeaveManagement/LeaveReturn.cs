using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LeaveManagement
{
  public  class LeaveReturn
    {
        public string leaveId { get; set; }
        public string employeeId { get; set; }
        public string returnType { get; set; }
        public string apStatus { get; set; }
        public DateTime date { get; set; }
        public string justification { get; set; }
        public string employeeName { get; set; }
        public string lrtName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
    public class DashboardLeave
    {
       
        public int leaveId { get; set; }
        public int employeeId { get; set; }
        public short status { get; set; }
        public string notes { get; set; }

    }
}

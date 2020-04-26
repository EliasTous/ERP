using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TimeAttendance
{
  public  class MailFlatShedule
    {
        public string FromDayId { get; set; }

        public string ToDayId { get; set; }

        public int EmployeeId { get; set; }
        public int BranchId { get; set; }
    }
}

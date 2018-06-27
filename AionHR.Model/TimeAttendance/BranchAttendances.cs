using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
  public  class BranchAttendance
    {
        public int branchId { get; set; }
        public string fromDayId { get; set; }
        public string toDayId { get; set; }
    }
}

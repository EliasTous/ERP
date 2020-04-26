using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TimeAttendance
{
    [ClassIdentifier("84109", "84")]
    public  class BranchAttendance
    {
        public int branchId { get; set; }
        public string fromDayId { get; set; }
        public string toDayId { get; set; }
        public int employeeId { get; set; }
    }
}

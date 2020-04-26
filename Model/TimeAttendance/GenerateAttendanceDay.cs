using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TimeAttendance
{
   
    public class GenerateAttendanceDay:ModelBase

    {
        public int employeeId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
       // public int branchId { get; set; }
    }
}

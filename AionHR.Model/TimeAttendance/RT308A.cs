using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class RT308A 
    {
        public string employeeRef { get; set; }
        public string employeeName { get; set; }
        public DateTime clockStamp { get; set; }
        public string udid { get; set; }
        public string serialNo { get; set; }
        public string recordId { get; set; }
    }
}

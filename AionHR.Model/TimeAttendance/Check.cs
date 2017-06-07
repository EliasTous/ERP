using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    [ClassIdentifier("41060", "41")]
    public class Check
    {
        public int authMode { get; set; }
        public int? employeeId { get; set; }

        public string employeeRef { get; set; }
        public int checkStatus { get; set; }
        public string ip { get; set; }
        public string serialNo { get; set; }

        public string clockStamp { get; set; }

        public string udId { get; set; }

    }
}

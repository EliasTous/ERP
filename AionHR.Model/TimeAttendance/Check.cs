using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    public class Check
    {
        public int? recordId;
        public DateTime clockStamp;
        public short authMode;
        public int employeeId;
        public short checkStatus;
        public double lat;
        public double lon;
        public string udid;
        public string routerRef;
        public string ip;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
  public  class OverrideTimeVariation
    {
        public string shiftId { get; set; }
        public string timeCode { get; set; }
        public string clockDuration { get; set; }
        public string damageLevel { get; set; }
        public string apStatus { get; set; }
        public string duration { get; set; }
        public DateTime clockStamp { get; set; }
        public string udid { get; set; }
        public string inOut { get; set; }
        public string employeeId { get; set; }
        public DateTime? date { get; set; }


    }
}

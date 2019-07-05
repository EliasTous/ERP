using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class TimePerformance
    {

        public int duration { get; set; }
        public int pending { get; set; }
        public int approved { get; set; }
        public int rejected { get; set; }
        public DateTime date { get; set; }
        public string dateString { get; set; }
    }
}

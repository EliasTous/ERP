using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class CalendarAlternation
    {
        public string caId { get; set; }

        public string year { get; set; }

        public string dayTypeId { get; set; }

        public int dow { get; set; }

        public DateTime dateFrom { get; set; }

        public DateTime dateTo { get; set; }

        public string startDayId { get; set; }

        public string endDayId { get; set; }


    }
}

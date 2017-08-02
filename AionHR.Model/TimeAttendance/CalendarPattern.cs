using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class CalendarPattern
    {
        public string caId
        {
            get; set;
        }

        public string year { get; set; }
        public DateTime dateFrom { get; set; }

        public DateTime dateTo { get; set; }

        public string scId { get; set; }

        public bool applyScId { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.TaskSchedule
{
    public class Report
    {
        public int taskId { get; set; }
        public int reportId { get; set; }
        public string reportName { get; set; }

        public string parameters { get; set; }
    }
}

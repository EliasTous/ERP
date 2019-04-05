using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
  public  class RT308
    {
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string dayId { get; set; }
        public DateTime dayIdDateTime { get; set; }
        public string[] punchLog { get; set; }
        public string punchString { get; set; }
        public string punchId { get; set; }
    }
}

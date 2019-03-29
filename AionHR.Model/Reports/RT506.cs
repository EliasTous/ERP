using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
  public  class RT506
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public double basicAmount { get; set; }
        public double eAmount { get; set; }
        public double dAmount { get; set; }
        public double essAmount { get; set; }
        public double netSalary { get; set; }
        public double cssAmount { get; set; }

    }
}

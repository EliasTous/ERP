using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
  public  class AdminDocumentDue
    {
        public string dayId { get; set; }
        public DateTime? dayIdDate { get; set; }
        public double amount   { get; set; }

        public string rowId { get; set; }
        public string doId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
   public class ReportParameter
    {
        public string id { get; set; }
        public string caption { get; set; }

        public int dataType { get; set; }
        public string data { get; set; }
        public int controlType { get; set; }
        public int classId { get; set; }
           
    }
}

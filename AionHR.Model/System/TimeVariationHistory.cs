using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
 public   class TimeVariationHistory:ModelBase
    {
        public string ttName { get; set; }
        public string userName { get; set; }
        public string classId { get; set; }
        public string masterRef { get; set; }

        public string userId { get; set; }
        public string type { get; set; }
        public DateTime eventDt { get; set; }
        public string data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    public class RT802
    {
        public int type { get; set; }
        public string TypeString { get; set; }
        public int classId { get; set; }

        public string ClassIdString { get; set; }
        public int pk { get; set; }
        public int userId { get; set; }
        public string data { get; set; }
        public string userName { get; set; }

        public DateTime eventDt { get; set; }



    }
}

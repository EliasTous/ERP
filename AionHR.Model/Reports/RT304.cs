using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
   public class RT304
    {
        public int dow { get; set; }

        public string from { get; set; }

        public string to { get; set; }

        public bool active { get; set; }
    }

    public class Period
    {
        public int periodId { get; set; }

        public string title { get; set; }

        public int count { get; set; }
    }

    public class WorkDay
    {
        public int dow { get; set; }

        public List<Period> periods { get; set; }
    }
}

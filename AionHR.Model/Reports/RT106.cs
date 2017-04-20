using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    public class RT106
    {

        public int year { get; set; }

        public int month { get; set; }

        public int terminated { get; set; }
        public double avgHC { get; set; }
        public double rate { get; set; }

        public string MonthString { get; set; }
    }
}

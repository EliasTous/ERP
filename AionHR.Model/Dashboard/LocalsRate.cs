using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
    [ClassIdentifier("82001", "82")]
    public class LocalsRate
    {
         public int employeeCount { get; set; }

        public int nationalCount { get; set; }

        public int netNationalCount { get; set; }

        public double rate { get; set; }
        public double netRate { get; set; }
        public double minLERate { get; set; }
        public double minNextLERate { get; set; }
        public string inName { get; set; }
        public string bsName { get; set; }
        public string leName { get; set; }


     

    }

}

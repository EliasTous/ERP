using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80801", "80")]
    public class RT801
    {

        public string userName { get; set; }

        public DateTime eventDT { get; set; }
        public String DateString { get; set; }
    }
}

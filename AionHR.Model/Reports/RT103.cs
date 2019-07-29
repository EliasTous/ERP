using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80103", "80")]
    public class RT103
    {
        public int headCount { get; set; }

        public DateTime date
        {
            set;
            get;
        }
        public string dateString
        {
            set;
            get;
        }
    }

   
}

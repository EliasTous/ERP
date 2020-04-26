using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80110", "80")]
    public class RT110
    {
        public int dimension { get; set; }

        public string name { get; set; }

        public int headCount { get; set; }
    }
}

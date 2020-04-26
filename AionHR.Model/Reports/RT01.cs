using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80101","80")]
    public class RT01
    {
        public int age00_18 { get; set; }

        public int age18_25 { get; set; }

        public int age26_30 { get; set; }
        public int age30_40 { get; set; }
        public int age40_50 { get; set; }
        public int age50_60 { get; set; } 
        public int age60_99 { get; set; }

        public string departmentName { get; set; }
    }
}

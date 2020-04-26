using Model.Access_Control;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80803", "80")]
    public class RT804
    {
        public string sgName { get; set; }
        public string moduleName { get; set; }
        public string className { get; set; }
        public string alName { get; set; }
    }
}

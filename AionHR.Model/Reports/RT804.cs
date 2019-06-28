using AionHR.Model.Access_Control;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
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

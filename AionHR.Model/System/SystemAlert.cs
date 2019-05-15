using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    [ClassIdentifier("20090", "20")]
    public class SystemAlert

    {
        [PropertyID("20090_01")]
        public bool isActive { get; set; }
        [PropertyID("20090_02")]
        public int alertId { get; set; }
        public short activeStatus { get; set; }

        //[PropertyID("20090_03")]
        //public short sendTo { get; set; }
        [PropertyID("20090_04")]
        public short days { get; set; }
        [PropertyID("20090_05")]
        public string description { get; set; }
        public bool predefined { get; set; }

        public string name { get; set; }

    }
}

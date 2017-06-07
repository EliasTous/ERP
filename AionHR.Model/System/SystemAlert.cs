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

        public bool isActive { get; set; }
        public int alertId { get; set; }

        

        public short sendTo { get; set; }

        public short days { get; set; }

        public string description { get; set; }
        public bool predefined { get; set; }

    }
}

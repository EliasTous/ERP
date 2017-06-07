using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    [ClassIdentifier("20000", "20")]
    public class SystemFolder:ModelBase
    {
        public string name { get; set; }
    }
}

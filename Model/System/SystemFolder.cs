using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{
    [ClassIdentifier("20000", "20")]
    public class SystemFolder:ModelBase
    {
        [ApplySecurity]
        [PropertyID("20000_01")]
        public string name { get; set; }
    }
}

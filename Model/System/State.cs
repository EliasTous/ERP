using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{
    [ClassIdentifier("20071","20")]
    public class State:ModelBase
    {
        [PropertyID("20071_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}

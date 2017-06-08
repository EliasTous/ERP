using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{

    public class State:ModelBase
    {
        [PropertyID("20020_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}

using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.NationalQuota
{
    [ClassIdentifier("10104", "10")]
    public class Citizenship:ModelBase
    {
        [PropertyID("10104_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("10104_02")]
        [ApplySecurity]
        public int ceiling { get; set;  }
        [PropertyID("10104_03")]
        [ApplySecurity]
        public double points { get; set; }
         
    }
}

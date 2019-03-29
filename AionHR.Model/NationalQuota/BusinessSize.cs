using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.NationalQuota
{
    [ClassIdentifier("10102", "10")]
    public   class BusinessSize:ModelBase
    {
        [PropertyID("10102_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("10102_02")]
        [ApplySecurity]
        public int minEmployees { get; set; }
        [PropertyID("10102_03")]
        [ApplySecurity]
        public int maxEmployees { get; set; }
    }
}

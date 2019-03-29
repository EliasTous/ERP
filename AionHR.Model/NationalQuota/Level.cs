using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.NationalQuota
{
    [ClassIdentifier("10103", "10")]
    public  class Level:ModelBase
    {
        [PropertyID("10103_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}

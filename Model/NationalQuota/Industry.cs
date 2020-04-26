using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.NationalQuota
{
    [ClassIdentifier("10101", "10")]
    public class Industry:ModelBase
    {
        [PropertyID("10101_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}

using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{
    [ClassIdentifier("20040","20")]
    public class Nationality:ModelBase
    {
        [PropertyID("20040_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("20040_02")]
        [ApplySecurity]
        public string ssId { get; set; }
        public string ssName { get; set; }

        public string bsId { get; set; }
    }
}

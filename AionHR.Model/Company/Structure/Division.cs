using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
    [ClassIdentifier("21010", "21")]
    public class Division:ModelBase
    {
        [PropertyID("21010_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("21010_02")]
        [ApplySecurity]
        public int timeZone { get; set; }
        [PropertyID("21010_03")]
        [ApplySecurity]
        public bool? isInactive { get; set; }
    }
}

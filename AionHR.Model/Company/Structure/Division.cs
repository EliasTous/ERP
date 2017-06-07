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
        public string name { get; set; }
        public int timeZone { get; set; }
        public bool? isInactive { get; set; }
    }
}

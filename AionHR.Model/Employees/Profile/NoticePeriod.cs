using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31140", "31")]
    public class NoticePeriod:ModelBase
    {
        [PropertyID("31140_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}

using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31021", "31")]
    public class CheckType:ModelBase
    {
        [PropertyID("31021_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}

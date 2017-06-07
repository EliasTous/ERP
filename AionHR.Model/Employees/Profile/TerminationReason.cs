using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31130", "31")]
    public class TerminationReason:ModelBase
    {
        public string name { get; set; }
    }
}

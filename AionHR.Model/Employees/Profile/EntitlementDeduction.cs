using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31061", "31")]
    public class EntitlementDeduction:ModelBase
    {
        public string name { get; set; }
        public short type { get; set; }
    }
}

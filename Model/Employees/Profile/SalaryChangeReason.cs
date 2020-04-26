using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31060", "31")]
    public class SalaryChangeReason:ModelBase
    {
        [PropertyID("31060_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}

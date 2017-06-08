using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31040", "31")]
    public class CertificateLevel : ModelBase
    {
        //public string reference { get; set; }
        [PropertyID("31040_01")]
        [ApplySecurity]
        public string name { get; set; }
    }
}

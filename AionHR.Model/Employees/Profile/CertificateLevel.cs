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
        public string name { get; set; }
    }
}

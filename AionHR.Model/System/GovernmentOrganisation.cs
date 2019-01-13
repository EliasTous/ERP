using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    [ClassIdentifier("20041", "20")]
    public class GovernmentOrganisation :ModelBase
    {
        public string name { get; set; }
    }
}

using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Benefits
{
    [ClassIdentifier("25012", "25")]
    public class BenefitsSchedule:ModelBase
    {
        public string name { get; set; }
    }
}

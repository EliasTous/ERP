using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    [ClassIdentifier("20020", "20")]
    public class Currency:ModelBase
    {
        public string name { get; set; }
        public string reference { get; set; }
        
    }
}

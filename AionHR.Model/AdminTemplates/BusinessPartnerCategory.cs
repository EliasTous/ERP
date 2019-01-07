using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
    
    [ClassIdentifier("70200", "70")]
    public  class BusinessPartnerCategory:ModelBase
    {

        public string name      { get; set; }

    }
}

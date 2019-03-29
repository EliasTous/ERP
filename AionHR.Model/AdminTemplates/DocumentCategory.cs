using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
    
     [ClassIdentifier("70300", "70")]
    public class DocumentCategory :ModelBase
    {
        public string name { get; set; }
    }
}

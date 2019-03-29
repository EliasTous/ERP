using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
    [ClassIdentifier("70102", "70")]
    public class TemplateTag
    
    {
        public int teId { get; set; }

        public string tag { get; set; }
    }
}

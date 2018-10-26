using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
    public class TemplateBody
    {
        [PropertyID("77000_01")]
        [ApplySecurity]
        public int teId { get; set; }
        [PropertyID("77000_02")]
        [ApplySecurity]
        public int languageId { get; set; }
        [PropertyID("77000_03")]
        [ApplySecurity]
        public string textBody { get; set; }
    }
}

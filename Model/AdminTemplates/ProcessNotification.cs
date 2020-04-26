using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AdminTemplates
{
    [ClassIdentifier("70103", "70")]
    public class ProcessNotification
    {
        public string teName { get; set; }
        public string processName { get; set; }
        public int? processId { get; set; }
        public string templateId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
    public class ProcessNotification
    {
        public string teName { get; set; }
        public string prName { get; set; }
        public int? processId { get; set; }
        public string templateId { get; set; }
    }
}

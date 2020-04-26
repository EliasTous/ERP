using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AdminTemplates
{
   public class MailEmployee :ModelBase
    {
        public string Params { get; set; }
        public string templateId { get; set; }
    }
}

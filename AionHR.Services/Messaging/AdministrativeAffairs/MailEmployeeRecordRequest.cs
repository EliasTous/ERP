using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.AdministrativeAffairs
{
   public class MailEmployeeRecordRequest : RecordRequest
    {
        public string param { get; set; }
        public string templateId { get; set; }


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_params", param);
                parameters.Add("_templateId", templateId);
                return parameters;
            }
        }
    }
}

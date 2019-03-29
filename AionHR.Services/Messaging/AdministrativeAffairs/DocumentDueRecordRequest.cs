using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.AdministrativeAffairs
{
 public   class DocumentDueRecordRequest:RecordRequest
    {
        public string dayId { get; set; }
        public string doId { get; set; }


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_dayId", dayId);
                parameters.Add("_doId", doId);
                return parameters;
            }
        }
    }
}

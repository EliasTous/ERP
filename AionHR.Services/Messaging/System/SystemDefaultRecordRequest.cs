using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
   public class SystemDefaultRecordRequest:RecordRequest
    {
        public string Key { get; set; }

        public override Dictionary<String, String> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_key", Key);
                return parameters;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
   public class BackgroundJobRecordRequest:RecordRequest
    {
        public string jobId { get; set; }
        private Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_jobId", jobId);

                return parameters;
            }
        }


    }
}

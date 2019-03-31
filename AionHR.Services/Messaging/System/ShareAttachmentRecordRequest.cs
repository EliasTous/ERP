using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
    class ShareAttachmentRecordRequest : RecordRequest
    {
        public string employeeId { get; set; }
        public string branchId { get; set; }
        public string type { get; set; }
        public string attachmentType { get; set; }

        public override Dictionary<String, String> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_key", type);
                return parameters;
            }
        }
    }
}
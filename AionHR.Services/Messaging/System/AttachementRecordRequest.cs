using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
  public   class AttachementRecordRequest: RecordRequest
    {
        public string classId { get; set; }
        public string seqNo { get; set; }




        public override Dictionary<string, string> Parameters
        {
            get
            {

                parameters = new Dictionary<string, string>();
                parameters = base.parameters;
                parameters.Add("_classId", classId);
                parameters.Add("_seqNo", seqNo);
                parameters.Add("_recordId", RecordID);
                return parameters;
            }
        }
    }
}

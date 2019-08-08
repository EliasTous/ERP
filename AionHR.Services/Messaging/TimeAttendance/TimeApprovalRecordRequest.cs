using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.TimeAttendance
{
   public class TimeApprovalRecordRequest :RecordRequest
    {

        public string seqNo { get; set; }
        public string timeCode { get; set; }
        public string shiftId { get; set; }

        public string tvId { get; set; }

        public Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                
                parameters = base.parameters;
                if (parameters == null)
                {
                    parameters = new Dictionary<string, string>();
                }
              
                parameters.Add("_seqNo", seqNo);
                parameters.Add("_timeCode", timeCode);
                parameters.Add("_shiftId", shiftId);
                parameters.Add("_tvId", tvId);

                return parameters;
            }
        }
    }
}

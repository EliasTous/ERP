using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.TimeAttendance
{
   public class TimeApprovalRecordRequest :RecordRequest
    {
        public string approverId { get; set; }
        public string employeeId { get; set; }
        public string dayId { get; set; }
        public string timeCode { get; set; }
        public string shiftId { get; set; }
        
      

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.parameters;
                parameters.Add("_approverId", approverId);
                parameters.Add("_employeeId", employeeId);
                parameters.Add("_dayId", dayId);
                parameters.Add("_timeCode", timeCode);
                parameters.Add("_shiftId", shiftId);

                return parameters;
            }
        }
    }
}

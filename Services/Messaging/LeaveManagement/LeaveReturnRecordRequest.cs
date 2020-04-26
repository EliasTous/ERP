using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.LeaveManagement
{
   public class LeaveReturnRecordRequest : RecordRequest
    {
        public string leaveId { get; set; }
       

        private Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_leaveId", leaveId);
            
                return parameters;
            }
        }
    }
}

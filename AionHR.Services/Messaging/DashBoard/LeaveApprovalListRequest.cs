using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.DashBoard
{
   public class LeaveApprovalListRequest :ListRequest
    {
        public short status { get; set; }
        public string approverId { get; set; }
        public string leaveId { get; set; }

        

        private Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_status", status.ToString());
                parameters.Add("_approverId", approverId);
                parameters.Add("_leaveId", leaveId);
                return parameters;
            }
        }
    }
}

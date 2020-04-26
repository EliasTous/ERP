using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.TimeAttendance
{
    public class PendingTimeApprovalListRequest : ListRequest
    {
        public string paramString { get; set; }
        public string Size { get; set; }
        public string StartAt { get; set; }
        public string sortBy { get; set; }



        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_params", paramString);
                parameters.Add("_size", Size);
                parameters.Add("_startAt", StartAt);
                parameters.Add("_sortBy", sortBy);
                
                return parameters;
            }
        }

    }
}

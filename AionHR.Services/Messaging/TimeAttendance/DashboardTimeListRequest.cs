using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.TimeAttendance
{
 public  class DashboardTimeListRequest:ListRequest
    {
        public int approverId { get; set; }
       
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_approverId", approverId.ToString());

           



                return parameters;
            }
        }
    }
}

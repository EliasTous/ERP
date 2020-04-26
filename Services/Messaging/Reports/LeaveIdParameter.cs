using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
  public  class LeaveIdParameter: ReportParameterSet
    {
        public string leaveId { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_leaveId", leaveId.ToString());
                return parameters;
            }
        }
    }
}

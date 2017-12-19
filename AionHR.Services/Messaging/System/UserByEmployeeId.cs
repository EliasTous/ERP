using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
  public  class UserByEmployeeId:RecordRequest
    {
        public string employeeId { get; set; }

        private Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_employeeId", employeeId);
                return parameters;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Employees
{
   public class EmployeePenaltyApprovalListRequest :ListRequest
    {
        public string penaltyId { get; set; }
        public string approverId { get; set; }

        public string apStatus { get; set; }

        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>(); 
                parameters.Add("_approverId", approverId);
                parameters.Add("_penaltyId", penaltyId);
                parameters.Add("_apStatus", apStatus);
              
                return parameters;
            }
        }
    }
}

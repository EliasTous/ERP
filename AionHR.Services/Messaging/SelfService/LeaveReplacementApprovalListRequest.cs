using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.SelfService
{
  
    public class LeaveReplacementApprovalListRequest : ListRequest
    {
        public string employeeId { get; set; }
        public string apStatus { get; set; }
    

        public string SortBy { get; set; }
     
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_employeeId", employeeId);
                parameters.Add("_apStatus", apStatus);
                parameters.Add("_SortBy", SortBy);
        
                //parameters.Add("_approverId", ApproverId.ToString());




                return parameters;
            }
        }
    }
}

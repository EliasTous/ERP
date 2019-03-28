using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.SelfService
{
  public  class AssetLoanListRequest:ListRequest
    {
        public string employeeId { get; set; }
        public string status { get; set; }




        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;

                parameters.Add("_employeeId", employeeId);
                parameters.Add("_status", status);

                return parameters;
            }
        }
    }
}

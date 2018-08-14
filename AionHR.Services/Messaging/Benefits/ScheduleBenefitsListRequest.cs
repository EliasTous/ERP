using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Benefits
{
 public  class ScheduleBenefitsListRequest:ListRequest
    {
        public string bsId { get; set; }

      


        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_bsId", bsId);
              

                return parameters;
            }
        }
    }
}

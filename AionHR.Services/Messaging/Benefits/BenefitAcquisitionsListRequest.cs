using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Benefits
{
   public class BenefitAcquisitionsListRequest :ListRequest
    {
        public int employeeId { get; set; }
        public int benefitId { get; set; }




        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_employeeId", employeeId.ToString());
                parameters.Add("_benefitId", benefitId.ToString());


                return parameters;
            }
        }
    }
}

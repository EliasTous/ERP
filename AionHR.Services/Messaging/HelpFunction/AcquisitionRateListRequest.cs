using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.HelpFunction
{
   public class AcquisitionRateListRequest : ListRequest
    {
        public string employeeId { get; set; }
        public string bsId { get; set; }
        public string benefitId { get; set; }


        public string asOfDate { get; set; }

        



        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;

                parameters.Add("_employeeId", employeeId);
                parameters.Add("_bsId", bsId);
                parameters.Add("_benefitId", benefitId);
                parameters.Add("_asOfDate", asOfDate.ToString());


                return parameters;
            }
        }
    }
}

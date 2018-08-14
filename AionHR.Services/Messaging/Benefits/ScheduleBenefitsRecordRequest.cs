using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Benefits
{
  public  class ScheduleBenefitsRecordRequest :RecordRequest
    {
        public string bsId { get; set; }
        public string benefitId { get; set; }

        public Dictionary<string, string> parameters;



        public override  Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_bsId", bsId);
                parameters.Add("_benefitId", benefitId);
                
                return parameters;
            }
        }
    }
}

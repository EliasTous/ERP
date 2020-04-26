using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
  public  class SponserParameterSet: ReportParameterSet
    {
        public int? sponsorId { get; set; }


        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = new Dictionary<string, string>();
                if (sponsorId.HasValue)
                    parameters.Add("_sponsorId", sponsorId.ToString());



                return parameters;
            }
        }
    }
}

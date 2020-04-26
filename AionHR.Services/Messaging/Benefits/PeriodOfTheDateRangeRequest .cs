using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Benefits
{
  public  class PeriodOfTheDateRangeRequest:RecordRequest
    {
        public string startDate { get; set; }
        public string endDate { get; set; }


    

        public override  Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters; 
                parameters.Add("_startDate", startDate);
                parameters.Add("_endDate", endDate);


                return parameters;
            }
        }

    }
}

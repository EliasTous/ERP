using AionHR.Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging
{
   public class TimeVariationListRequest : DashboardRequest
    {


        public string timeCode { get; set; }
        
        public DateTime fromDayId { get; set; }

        public DateTime toDayId { get; set; }
        public string employeeId { get; set; }
        public string apStatus { get; set; }
        public string fromDuration { get; set; }
        public string toDuration { get; set; }


       

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;

                parameters.Add("_timeCode", timeCode);
                parameters.Add("_fromDayId",fromDayId.ToString("yyyyMMdd"));
                parameters.Add("_toDayId", toDayId.ToString("yyyyMMdd"));
                parameters.Add("_employeeId", employeeId);
                parameters.Add("_apStatus", apStatus);
                parameters.Add("_fromDuration", fromDuration);
                parameters.Add("_toDuration", toDuration);




                return parameters;
            }
        }
    }
}

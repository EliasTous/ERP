using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.DashBoard
{
   public class TimeVariationListRequest :ListRequest
    {
        public string paramString { get; set; }
        public string timeCode { get; set; }

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();


                parameters.Add("_params", paramString);
                parameters.Add("_timeCode", timeCode);
                

                return parameters;
            }
        }
    }
}

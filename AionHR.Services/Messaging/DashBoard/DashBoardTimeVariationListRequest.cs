using AionHR.Services.Messaging.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.DashBoard
{
   public class DashBoardTimeVariationListRequest : DashboardRequest
    {


        public string timeVariationType { get; set; }
        




        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;

                parameters.Add("_timeVariationType", timeVariationType);
             


                return parameters;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class PendingPunchParameterSet : ListRequest
    {
        public string ppType { get; set; }
        public string udid { get; set; }



        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_ppType", ppType);
                parameters.Add("_udid", udid);


                return parameters;
            }
        }
    }
}

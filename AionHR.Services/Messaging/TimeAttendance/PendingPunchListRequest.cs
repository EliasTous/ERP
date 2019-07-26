using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.TimeAttendance
{



    public class PendingPunchListRequest : ListRequest
    {
        public string ppTypeParam { get; set; }





        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.parameters;

                parameters.Add("_ppType", ppTypeParam);


                return parameters;
            }
        }
    }
}

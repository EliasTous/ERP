using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.System
{
  public  class TimeVariationHistoryListRequest : ListRequest
    {
        public string userId { get; set; }
        public string classId { get; set; }
        public string type { get; set; }
        public string masterRef { get; set; }


        private Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_userId", userId);
                parameters.Add("_classId", classId);
                parameters.Add("_type", type);
                parameters.Add("_masterRef", masterRef);


                return parameters;
            }
        }
    }
}
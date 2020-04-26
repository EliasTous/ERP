using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.System
{
   public  class ApplyLetterRecordRequest :RecordRequest
    {
        public int? employeeId { get; set; }
        public int ltId { get; set; }
        

        private Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_employeeId", employeeId.ToString());
                parameters.Add("_ltId", ltId.ToString());
                return parameters;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.SelfService
{
    public class LetterSelfServiceListRequest:ListRequest
    {
        public string employeeId { get; set; }
        public string sortBy { get; set; }



     
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;

                parameters.Add("_employeeId", employeeId);
                parameters.Add("_sortBy", sortBy);

                return parameters;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Employees
{
   public class UserPropertyRecordRequest:RecordRequest
    {
        public string propertyId { get; set; }
       



        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_propertyId", propertyId);
               


                return parameters;
            }
        }
    }
}

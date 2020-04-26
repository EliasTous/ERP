using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Employees
{
  public  class EmployeeUserValueRecordRequest:RecordRequest
    {
        public string propertyId { get; set; }
        public string employeeId { get; set; }
        



        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_propertyId", propertyId);
                parameters.Add("_employeeId", employeeId);




                return parameters;
            }
        }
    }
   
}

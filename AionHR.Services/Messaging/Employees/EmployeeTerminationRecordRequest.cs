using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Employees
{
    public class EmployeeTerminationRecordRequest : RecordRequest
    {

        public string employeeId { get; set; }




        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();

                parameters.Add("_employeeId", employeeId);




                return parameters;
            }
        }
    }
}
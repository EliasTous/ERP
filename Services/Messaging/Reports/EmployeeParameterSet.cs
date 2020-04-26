using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
    public class EmployeeParameterSet:ReportParameterSet
    {
        public int employeeId { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_employeeId", employeeId.ToString());
                return parameters;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class EmployeeStatusParameterSet:ReportParameterSet
    {

        public int EmploymentStatusId { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_ehId", EmploymentStatusId.ToString());
                return parameters;
            }
        }
    }
}

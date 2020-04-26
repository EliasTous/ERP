using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
  public  class EmploymentStatusParameterSet:ReportParameterSet
    {
        public int esId { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_esId", esId.ToString());
                return parameters;
            }
        }
    }
}

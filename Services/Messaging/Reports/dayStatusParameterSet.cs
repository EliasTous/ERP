using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
  public  class dayStatusParameterSet:ReportParameterSet
    {
        public string punchStatus { get; set; }
        public string dayStatus { get; set; }


        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_punchStatus", punchStatus);
                parameters.Add("_dayStatus", dayStatus);


                return parameters;
            }
        }
    }
}

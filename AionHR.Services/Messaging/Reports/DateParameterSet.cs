using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
  public   class DateParameterSet:ReportParameterSet
    {

        public DateTime Date { get; set; }

        protected Dictionary<string, string> parameters;
        public override Dictionary<String, String> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_asOfDate", Date.ToShortDateString());
                return parameters;
            }
        }
    }
}

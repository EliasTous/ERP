using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
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
                parameters.Add("_asOfDate", Date.ToString("MM/dd/yyyy", new CultureInfo("en-US")));
                return parameters;
            }
        }
    }
}

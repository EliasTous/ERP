using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
    public class DayIdParameterSet : ReportParameterSet
    {

        public DateTime Date { get; set; }

        protected Dictionary<string, string> parameters;
        public override Dictionary<String, String> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_dayId", Date.ToString("yyyyMMdd"));
                return parameters;
            }
        }
    }
}

    

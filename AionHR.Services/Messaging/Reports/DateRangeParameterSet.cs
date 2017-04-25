using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class DateRangeParameterSet:ReportParameterSet
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();

                parameters.Add("_fromDate", DateFrom.ToString("MM/dd/yyyy",new CultureInfo("en-US")));
                parameters.Add("_toDate", DateTo.ToString("MM/dd/yyyy", new CultureInfo("en-US")));

                return parameters;
            }
        }
    }
}

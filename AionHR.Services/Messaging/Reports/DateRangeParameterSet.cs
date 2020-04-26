using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
    public class DateRangeParameterSet:ReportParameterSet
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        protected Dictionary<string, string> parameters;
        
        public bool IsDayId { get; set; }
        public DateRangeParameterSet()
        {
            IsDayId = false;
           
        }

        public override Dictionary<string, string> Parameters
        {
            get
            {
               string targetFormat = "yyyy/MM/dd";
                parameters = new Dictionary<string, string>();
                if(IsDayId)
                {
                    targetFormat = "yyyyMMdd";
                    parameters.Add("_fromDayId", DateFrom.ToString(targetFormat, new CultureInfo("en-US")));
                    parameters.Add("_toDayId", DateTo.ToString(targetFormat, new CultureInfo("en-US")));
                }
                else
                {
                    parameters.Add("_fromDate", DateFrom.ToString(targetFormat, new CultureInfo("en-US")));
                    parameters.Add("_toDate", DateTo.ToString(targetFormat, new CultureInfo("en-US")));

                }
              

                return parameters;
            }
        }
    }
}

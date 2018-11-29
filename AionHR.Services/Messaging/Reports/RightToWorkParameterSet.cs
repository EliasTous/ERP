using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class RightToworkReportParameter : ReportParameterSet
    {
        public string status { get; set; }
        public string dtId { get; set; }
        public string esId { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_status", status.ToString());
                parameters.Add("_dtId", dtId.ToString());
                parameters.Add("_esId", esId.ToString());
                return parameters;
            }
        }
    }
}
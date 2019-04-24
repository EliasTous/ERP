using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class RightToworkReportParameter : ReportParameterSet
    {
      
        public string dtId { get; set; }
        public string esId { get; set; }
        public DateTime expiryAfter { get; set; }
        public DateTime expiryBefore { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
            
                parameters.Add("_dtId", dtId.ToString());
                parameters.Add("_esId", esId.ToString());
                parameters.Add("_expiryAfter", expiryAfter.ToString());
                parameters.Add("_expiryBefore",expiryBefore.ToString());
                return parameters;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
   public class BranchWorkforceParameterSet:ReportParameterSet
    {
        public string goId { get; set; }
        public DateTime asOfDate { get; set; }
       

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_goId", goId);
                parameters.Add("_asOfDate", asOfDate.ToString());

                return parameters;
            }
        }
    }
}

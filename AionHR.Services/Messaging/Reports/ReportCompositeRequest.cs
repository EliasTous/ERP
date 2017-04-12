using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class ReportCompositeRequest:ListRequest
    {
        private List<ReportParameterSet> parametersSet;

        public ReportCompositeRequest()
        {
            parametersSet = new List<ReportParameterSet>();
        }
        public string SortBy { get; set; }
        public void Add(ReportParameterSet param)
        {
            parametersSet.Add(param);
        }

        
        protected Dictionary<string, string> parameters;
  

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_sortBy", SortBy);
                foreach (var item in parametersSet)
                {
                    var temp = item.Parameters;
                    foreach (var inner in temp)
                    {
                        parameters.Add(inner.Key, inner.Value);
                    }
                }
                return parameters;
            }
        }


    }
}

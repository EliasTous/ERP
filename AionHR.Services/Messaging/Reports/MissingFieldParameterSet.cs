using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class MissingFieldParameterSet : ReportParameterSet
    {
        public string fieldId { get; set; }

        protected Dictionary<string, string> parameters;
        public override Dictionary<String, String> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_fieldId", fieldId);
                return parameters;
            }
        }
    }
}

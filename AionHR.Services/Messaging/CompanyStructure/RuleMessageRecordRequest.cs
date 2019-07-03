using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.CompanyStructure
{
 public   class RuleMessageRecordRequest :RecordRequest
    {
        public string ruleId { get; set; }
        public string languageId { get; set; }

        public Dictionary<string, string> parameters;



        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_ruleId", ruleId);
                parameters.Add("_languageId", languageId);

                return parameters;
            }
        }
    }
}

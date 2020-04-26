using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.CompanyStructure
{
   public class RuleTriggerListRequset:ListRequest
    {
        public string ruleId { get; set; }
        public string accessType { get; set; }
        public string classId { get; set; }




        /// <summary>
        /// /// parameter list shipped with the web request
        /// </summary>
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = new Dictionary<string, string>();


                parameters.Add("_ruleId", ruleId);
                parameters.Add("_accessType", accessType);
                parameters.Add("_classId", classId);


                return parameters;
            }
        }
    }
}

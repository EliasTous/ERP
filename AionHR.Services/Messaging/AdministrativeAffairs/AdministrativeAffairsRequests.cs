using AionHR.Model.AdminTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging
{
public class TemplateBodyListReuqest:ListRequest
    {
        public int TemplateId { get; set; }

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_teId", TemplateId.ToString());
                

                return parameters;
            }
        }
    }
    public class TemplateBodyRecordRequest : RecordRequest
    {
        public int TemplateId { get; set; }
        public int LanguageId { get; set; }
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_teId", TemplateId.ToString());
                parameters.Add("_languageId", LanguageId.ToString());

                return parameters;
            }
        }
    }
   
}

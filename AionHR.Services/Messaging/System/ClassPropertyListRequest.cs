using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
  public  class ClassPropertyListRequest:ListRequest
    {
        public string classId { get; set; }

        public string moduleId { get; set; }

       

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_classId", classId);
                parameters.Add("_moduleId", moduleId);
                return parameters;
            }
        }
    }


    public class ReportsListRequest : ListRequest
    {
        public string moduleId { get; set; }



        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();                
                parameters.Add("_moduleId", moduleId);
                return parameters;
            }
        }
    }
}

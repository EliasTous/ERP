using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.System
{
   public class XMLDictionaryListRequest:ListRequest
    {
        public string database { get; set; }

     

        private Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_database", database);
              
                return parameters;
            }
        }
    }
}


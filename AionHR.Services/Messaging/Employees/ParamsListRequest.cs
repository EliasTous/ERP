using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Employees
{
   public class ParamsListRequest:ListRequest
    {
       
      

        public string param { get; set; }

        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
               
                parameters = new Dictionary<string, string>();
                parameters.Add("_params", param);
              

                return parameters;
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Payroll
{
   public class CheckExpressionRecordRequest:RecordRequest
    {
        public string expression { get; set; }




        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_expression", expression);



                return parameters;
            }
        }


    }

    public class CheckNamesRecordRequest : RecordRequest
    {
        public string name { get; set; }




        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_expression", name);



                return parameters;
            }
        }


    }
}

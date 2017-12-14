using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
 public  class GenderParameterSet : ReportParameterSet
    {
        public short? gender { get; set; }


        protected Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {

            get
            {
                parameters = new Dictionary<string, string>();
                if (gender.HasValue)
                    parameters.Add("_gender", gender.ToString());
               
                

                return parameters;
            }
        }
    }
}

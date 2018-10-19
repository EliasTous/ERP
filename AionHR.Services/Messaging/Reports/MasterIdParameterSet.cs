using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
  public  class MasterIdParameterSet : ReportParameterSet
    {
        public int masterRef { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_masterRef", masterRef.ToString());
                return parameters;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
  public  class MasterIdParameterSet : ReportParameterSet
    {
        public string masterRef { get; set; }

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


    public class DataParameterSet : ReportParameterSet
    {
        public string data { get; set; }

        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_data", data.ToString());
                return parameters;
            }
        }
    }
}

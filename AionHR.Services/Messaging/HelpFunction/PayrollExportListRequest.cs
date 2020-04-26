using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.HelpFunction
{
 public   class PayrollExportListRequest :ListRequest
    { 
    public string payRef { get; set; }
  





    public override Dictionary<string, string> Parameters
    {
        get
        {
            parameters = base.Parameters;

            parameters.Add("_payRef", payRef);
          


            return parameters;
        }
    }
}
}

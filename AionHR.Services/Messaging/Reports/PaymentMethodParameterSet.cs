using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Reports
{
    public class PaymentMethodParameterSet:ReportParameterSet
    {
        public int paymentMethod { get; set; }
        public int payref { set; get; }

        
        protected Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_paymentMethod", paymentMethod.ToString());
                parameters.Add("_payref", payref.ToString());
                return parameters;
            }
        }
    }
}

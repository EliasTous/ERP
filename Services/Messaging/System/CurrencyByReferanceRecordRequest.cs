using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.System
{
public    class CurrencyByReferanceRecordRequest :RecordRequest

    {
        public string Reference { get; set; }
        private Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_reference", Reference);

                return parameters;
            }
        }
    }
}

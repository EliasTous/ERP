﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Reports
{
    public class ReportGenericRequest:ListRequest
    {
        public string paramString { get; set; }

       

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_params", paramString);
                return parameters;
            }
        }

    }
}

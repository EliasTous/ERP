﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.NationalQuotas
{
   
    public class LevelAcquisitionListRequest:ListRequest
        {
        public int BusinessSizeId { get; set; }

        public int industryId { get; set; }
        
        private Dictionary<string, string> parameters;
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_bsId", BusinessSizeId.ToString());
                parameters.Add("_inId", industryId.ToString());
              
                return parameters;
            }
        }

    }
}

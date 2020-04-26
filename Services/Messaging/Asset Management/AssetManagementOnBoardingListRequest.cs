using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Asset_Management
{
 public   class AssetManagementOnBoardingListRequest:ListRequest
    {
        public string categoryId { get; set; }
        public string positionId { get; set; }
        
        public override Dictionary<string, string> Parameters
        {
            get
            {
                if (string.IsNullOrEmpty(positionId))
                    positionId = "0";
                if (string.IsNullOrEmpty(categoryId))
                    categoryId = "0";

                parameters = base.Parameters;
                parameters.Add("_categoryId", categoryId);
              
                parameters.Add("_positionId", positionId);



                return parameters;
            }
        }
    }
}


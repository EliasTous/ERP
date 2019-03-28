using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Asset_Management
{
   public class AssetManagementCategoryPropertyListRequest:ListRequest
    {
        public string categoryId { get; set; }
       
        public override Dictionary<string, string> Parameters
        {
            get
            {
                if (string.IsNullOrEmpty(categoryId))
                    categoryId = "0";
                parameters = base.Parameters;

                parameters.Add("_categoryId", categoryId);
               


                return parameters;
            }
        }
    }

    public class AssetPropertyValueListRequest : ListRequest
    {
        public string categoryId { get; set; }
        public string assetId { get; set; }
        public override Dictionary<string, string> Parameters
        {
            get
            {
                
                parameters = base.Parameters;

                parameters.Add("_categoryId", categoryId);
                parameters.Add("_assetId", assetId);


                return parameters;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Asset_Management
{
  public  class AssetManagementCategoryPropertyRecordRequest:RecordRequest
    {
        public string categoryId { get; set; }
        public string propertyId { get; set; }
        private Dictionary<string, string> parameters;

        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = new Dictionary<string, string>();
                parameters.Add("_categoryId", categoryId);
                parameters.Add("_propertyId", propertyId);

                return parameters;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.Asset_Management
{
 public   class AssetManagementPurchaseOrdersListRequest:ListRequest
    {
        public string categoryId { get; set; }
        public string apStatus { get; set; }
        public string status { get; set; }
        public string supplierId { get; set; }
        public string branchId { get; set; }
        public override Dictionary<string, string> Parameters
        {
            get
            {
                if (string.IsNullOrEmpty(status))
                    status = "0";
                if (string.IsNullOrEmpty(apStatus))
                    apStatus = "0";

                parameters = base.Parameters;
                parameters.Add("_categoryId", categoryId);
                parameters.Add("_supplierId", supplierId);
                parameters.Add("_branchId", branchId);
                parameters.Add("_apStatus", apStatus);
                parameters.Add("_status", status);



                return parameters;
            }
        }
    }
}

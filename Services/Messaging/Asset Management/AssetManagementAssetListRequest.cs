﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messaging.Asset_Management
{
 public   class AssetManagementAssetListRequest : ListRequest
    {
        public string categoryId { get; set; }
        public string positionId { get; set; }
        public string employeeId { get; set; }
        public string departmentId { get; set; }
        public string supplierId { get; set; }
        public string branchId { get; set; }
        public string PurchaseOrderId { get; set; }
        public override Dictionary<string, string> Parameters
        {
            get
            {
                if (string.IsNullOrEmpty(PurchaseOrderId))
                    PurchaseOrderId = "0";
                parameters = base.Parameters;
                parameters.Add("_categoryId", categoryId);
                parameters.Add("_positionId", positionId);
                parameters.Add("_employeeId", employeeId);
                parameters.Add("_departmentId", departmentId);
                parameters.Add("_supplierId", supplierId);
                parameters.Add("_branchId", branchId);
                parameters.Add("_poRef", PurchaseOrderId);



                return parameters;
            }
        }
    }
}

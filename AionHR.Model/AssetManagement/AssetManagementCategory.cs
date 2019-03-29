using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AssetManagement
{
   public class AssetManagementCategory :ModelBase
    {
        public string name { get; set; }
        public int? deliveryDuration { get; set; }
        public string apId { get; set; }
        public string parentId { get; set; }
        public int? defaultDepreciationYears { get; set; }
        public int? defaultWarrantyYears { get; set; }

    }
}

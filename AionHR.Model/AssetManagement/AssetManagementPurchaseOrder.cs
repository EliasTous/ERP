using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AssetManagement
{
  public  class AssetManagementPurchaseOrder :ModelBase
    {
        public string supplierName { get; set; }
        public string categoryName { get; set; }
        public string currencyName { get; set; }
        public DateTime? date { get; set; }
        public string categoryId { get; set; }
        public int? qty { get; set; }
        public string supplierId { get; set; }
        public string comments { get; set; }
        public string apStatus { get; set; }
        public short? status { get; set; }
        public string currencyId { get; set; }
        public double? costPrice { get; set; }
        public string branchId { get; set; }
        public string branchName { get; set; }
        public string departmentId { get; set; }
        public string departmentName { get; set; }
    }
}

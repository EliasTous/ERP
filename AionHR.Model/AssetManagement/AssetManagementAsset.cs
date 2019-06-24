using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AssetManagement
{
  public  class AssetManagementAsset:ModelBase
    {
        
        public string assetRef { get; set; }
        public string categoryName { get; set; }
        public string supplierName { get; set; }
        public string branchName { get; set; }
        public string departmentName { get; set; }
        public string employeeName { get; set; }
        public string serialNo { get; set; }
        public string supplierId { get; set; }
        public string categoryId { get; set; }
        public string currencyId { get; set; }
        public double? costPrice { get; set; }
        public string poRef { get; set; }
        public DateTime? warrantyEndDate { get; set; }
        public DateTime? depreciationDate { get; set; }
        public DateTime? receptionDate { get; set; }
        public string condition { get; set; }
        public string branchId { get; set; }
       
        
        public string comments { get; set; }

        public int? status { get; set; }
        public string employeeId { get; set; }
        public string departmentId { get; set; }
       
       
        public string name { get; set; }
        public string statusName { get; set; }
    }
}

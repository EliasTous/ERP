using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AssetManagement
{
  public  class AssetManagementPurchaseOrderApproval
    {
        public string approverName { get; set; }
        public string departmentName { get; set; }
        public string branchName { get; set; }
        public string categoryName { get; set; }
        public int? qty { get; set; }
        public int? poId { get; set; }
        public string approverId { get; set; }
        public short? status { get; set; }
        public string statusString { get; set; }
        public string Comments { get; set; }
        public string arId { get; set; }
        public string arName { get; set; }






    }
}

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
        public int? poId { get; set; }
        public int? approverId { get; set; }
        public short? status { get; set; }
        public string statusString { get; set; }
        public string notes { get; set; }
        public EmployeeName employeeName { get; set; }
        public EmployeeName  approverName { get; set; }
        public string departmentName { get; set; }
        public string branchName { get; set; }
        public string categoryName { get; set; }
        public int? qty { get; set; }
       
    }
}

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
        public EmployeeName approverName { get; set; }
        //public EmployeeName employeeName { get; set; }
        //public double amount { get; set; }
        //public string currencyRef { get; set; }

        public string departmentName { get; set; }
        public string poId { get; set; }
        public int approverId { get; set; }
        public int employeeId { get; set; }

        public string notes { get; set; }
        public string statusString { get; set; }
        public short status { get; set; }
    }
}

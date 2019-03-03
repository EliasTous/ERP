using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AssetManagement
{
  public  class AssetManagementLoan :ModelBase
    {
        public EmployeeName employeeName { get; set; }
        public short? status { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }
        public string branchName { get; set; }
        public DateTime date { get; set; }
        public string employeeId { get; set; }
        public string assetId { get; set; }
        public string comments { get; set; }

    }
}

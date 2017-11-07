using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
  public  class EmpRTW
    {

        public EmployeeName employeeName { get; set; }
        public string dtName { get; set; }

        public string documentRef { get; set; }

        public DateTime expiryDate { get; set; }

        public int days { get; set; }
       public string positionName { get; set;  }
        public string departmentName { get; set;  }
        public string branchName { set; get;  }


    }
}

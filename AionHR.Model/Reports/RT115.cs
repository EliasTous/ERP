using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
  public  class RT115
    {
        public string employeeName { get; set; }
        public DateTime? hireDate { get; set; }
        public DateTime? terminationDate { get; set; }
        public string serviceDuration { get; set; }
        public string branchName { get; set; }
        public string departmentName { get; set; }
        public string jobPositionName { get; set; }
        public double indemnityAmount { get; set; }
        public double salary { get; set; }
        public string hireDateString { get; set; }
        public string terminationDateString { get; set; }

    }
}

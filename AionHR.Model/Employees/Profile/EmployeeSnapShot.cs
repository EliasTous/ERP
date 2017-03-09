using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
   public class EmployeeSnapShot:ModelBase
    {
        public string reference { get; set; }
        public string fullName { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }
        public string branchName { get; set; }
        public DateTime hireDate { get; set; }
        public bool isInactive { get; set; }
        public DateTime? lastTACheck { get; set; }
        public short lastTACheckStatus { get; set; }
    }
}

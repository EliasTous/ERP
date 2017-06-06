using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class JobInfo:ModelBase
    {
        
        public DateTime date { get; set; }
        public int departmentId { get; set; }
        public int branchId { get; set; }
        public int? divisionId { get; set; }
        public int positionId { get; set; }

        public int? reportToId { get; set; }
        
        public string notes { get; set; }
        // qry
        public EmployeeName employeeName;
        public string departmentName { get; set; }
        public string branchName { get; set; }
        public string positionName { get; set; }
        public string divisionName { get; set; }

        public EmployeeName reportToName { get; set; }
        public int employeeId { get; set; }

    }
}

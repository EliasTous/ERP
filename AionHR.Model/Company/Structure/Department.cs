using AionHR.Infrastructure.Domain;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
    public class Department :ModelBase, IEntity
    {
        public string departmentRef { get; set; }
        public string name { get; set; }
        public string parentName { get; set; }
        public int? parentId { get; set; }
        public int? supervisorId { get; set; }
        public EmployeeName supervisorName { get; set; }

        public bool? isInactive { get; set; }
        
    }
}

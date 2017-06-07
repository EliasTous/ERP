using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
    [ClassIdentifier("21040", "21")]
    public class Department : ModelBase, IEntity
    {
        public string name { get; set; }
        public string departmentRef { get; set; }
        public bool? isInactive { get; set; }
        public string parentName { get; set; }
        public int? supervisorId { get; set; }
        public int? parentId { get; set; }

        public EmployeeName supervisorName { get; set; }



    }
}

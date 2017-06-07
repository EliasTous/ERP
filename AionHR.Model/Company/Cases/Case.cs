using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Cases
{
    [ClassIdentifier("43000", "43")]
    public  class Case : ModelBase,IEntity
    {
        
        public int employeeId { get; set; }
        
        public DateTime date { get; set; }
        public string details { get; set; }
        public short status { get; set; }
        public DateTime? closedDate { get; set; }
        public EmployeeName employeeName { get; set; }
    }
}

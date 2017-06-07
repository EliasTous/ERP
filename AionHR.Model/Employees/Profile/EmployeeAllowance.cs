using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31011", "31")]
    public class EmployeeAllowance:ModelBase
    {
        public int employeeId { get; set; }
        public int atId { get; set; }
        public DateTime date { get; set; }
        public string notes { get; set; }
        public double amount { get; set; }
    }
}

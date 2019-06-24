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
        [PropertyID("43000_01")]
        [ApplySecurity]
        public int employeeId { get; set; }
        [PropertyID("43000_02")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("43000_03")]
        [ApplySecurity]
        public string details { get; set; }
        [PropertyID("43000_04")]
        [ApplySecurity]
        public short status { get; set; }
        [PropertyID("43000_05")]
        [ApplySecurity]
        public DateTime? closedDate { get; set; }
        [PropertyID("43000_01")]
        [ApplySecurity]
        public string employeeName { get; set; }
    }
}

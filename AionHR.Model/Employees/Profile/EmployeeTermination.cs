using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31131", "31")]
    public class EmployeeTermination:ModelBase
    {

        [PropertyID("31131_01")]
        [ApplySecurity]
        public DateTime? date { get; set; }
        [PropertyID("31131_02")]
        [ApplySecurity]
        public int? ttId { get; set; }
        [PropertyID("31131_03")]
        [ApplySecurity]
        public int? trId { get; set; }

        [PropertyID("31131_04")]
        [ApplySecurity]
        public int? rehire { get; set; }
        

        public int? employeeId { get; set; }
    }
}

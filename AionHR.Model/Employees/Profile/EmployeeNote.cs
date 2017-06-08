using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31090", "31")]

    public class EmployeeNote:ModelBase
    {
        [PropertyID("31090_01")]
        [ApplySecurity]
        public string userId { get; set; }
        [PropertyID("31090_02")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("31090_03")]
        [ApplySecurity]
        public string note { get; set; }




        [PropertyID("31090_01")]
        [ApplySecurity]
        public string userName { get; set; }

        public string employeeId { get; set; }

        public string employeeRef { get; set; }

    }
}

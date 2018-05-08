using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31071", "31")]
    public class EmploymentHistory :ModelBase
    {

        [PropertyID("31071_01")]
        [ApplySecurity]
        public int statusId { get; set; }


        [PropertyID("31071_02")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("31071_03")]
        [ApplySecurity]
        public string comment { get; set; }
        public int employeeId { get; set; }

        public EmployeeName employeeName { get; set; }
        [PropertyID("31071_01")]
        [ApplySecurity]
        public string statusName { get; set; }

        public string employeeRef { get; set; }
        public string statusRef { get; set; }

    }

    [ClassIdentifier("31070", "31")]
    public  class EmploymentStatus : ModelBase

    {
        [PropertyID("31070_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("31070_01")]
        [ApplySecurity]
        public string esRef { get; set; }
        [PropertyID("31070_02")]
        [ApplySecurity]
        public bool excludeSS { get; set; }

     
    }
}

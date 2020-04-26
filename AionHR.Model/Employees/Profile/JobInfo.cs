using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31080", "31")]
    public class JobInfo:ModelBase
    {
        [PropertyID("31080_01")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("31080_02")]
        [ApplySecurity]
        public int departmentId { get; set; }
        [PropertyID("31080_03")]
        [ApplySecurity]
        public int branchId { get; set; }
        [PropertyID("31080_04")]
        [ApplySecurity]
        public int? divisionId { get; set; }
        [PropertyID("31080_05")]
        [ApplySecurity]
        public int positionId { get; set; }
        [PropertyID("31080_06")]
        [ApplySecurity]
        public int? reportToId { get; set; }
        [PropertyID("31080_07")]
        [ApplySecurity]
        public string notes { get; set; }
        // qry
        public string employeeName;
        [PropertyID("31080_02")]
        [ApplySecurity]
        public string departmentName { get; set; }
        [PropertyID("31080_03")]
        [ApplySecurity]
        public string branchName { get; set; }
        [PropertyID("31080_05")]
        [ApplySecurity]
        public string positionName { get; set; }
        [PropertyID("31080_04")]
        [ApplySecurity]
        public string divisionName { get; set; }

        public string reportToName { get; set; }
        public int employeeId { get; set; }

        public string employeeRef { get; set; }

        public string reportToRef { get; set; }

    }
}

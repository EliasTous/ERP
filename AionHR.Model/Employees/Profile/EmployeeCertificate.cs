using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31041", "31")]
    public class EmployeeCertificate:ModelBase
    {
        [PropertyID("31041_01")]
        [ApplySecurity]
        public string institution { get; set; }
        [PropertyID("31041_02")]
        [ApplySecurity]
        public int clId { get; set; }

        [PropertyID("31041_03")]
        [ApplySecurity]

        public DateTime? dateFrom { get; set; }
        [PropertyID("31041_04")]
        [ApplySecurity]
        public DateTime? dateTo { get; set; }

        [PropertyID("31041_05")]
        [ApplySecurity]
        public string grade { get; set; }
        [PropertyID("31041_06")]
        [ApplySecurity]
        public string major { get; set; }

        public int employeeId { get; set; }
        public string employeeName { get; set; }
        [PropertyID("31041_02")]
        [ApplySecurity]
        public string clName { get; set; }
    }
}

using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31020", "31")]
    public class EmployeeBackgroundCheck:ModelBase
    {
        
        public string employeeName { get; set; }
        [PropertyID("31020_01")]
        [ApplySecurity]
        public int ctId { get; set; }
        [PropertyID("31020_01")]
        [ApplySecurity]
        public string ctName { get; set; }
        [PropertyID("31020_02")]
        [ApplySecurity]
        public string remarks { get; set; }
        [PropertyID("31020_03")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("31020_04")]
        [ApplySecurity]
        public DateTime expiryDate { get; set; }
        [PropertyID("31020_05")]
        [ApplySecurity]
        public string fileUrl { get; set; }

        public int employeeId { get; set; }
    }
}

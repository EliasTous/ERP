using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51020", "51")]
    public class GenerationHeader:ModelBase
    {
        [PropertyID("51020_01")]
        [ApplySecurity]
        public string payRef { get; set; }
        [PropertyID("51020_02")]
        [ApplySecurity]
        public string fiscalYear { get; set; }
        [PropertyID("51020_03")]
        [ApplySecurity]
        public string salaryType { get; set; }
        [PropertyID("51020_04")]
        [ApplySecurity]
        public string periodId { get; set; }
        [PropertyID("51020_05")]
        [ApplySecurity]
        public string payDate { get; set; }
        [PropertyID("51020_06")]
        [ApplySecurity]
        public string status { get; set; }
        [PropertyID("51020_07")]
        [ApplySecurity]
        public string notes { get; set; }
        [PropertyID("51020_08")]
        [ApplySecurity]
        public DateTime startDate { get; set; }
        [PropertyID("51020_09")]
        [ApplySecurity]
        public DateTime endDate { get; set; }
        public string payRefWithDateRange { get; set;  }
       
    }
}

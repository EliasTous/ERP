using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51120", "51")]
    public class GenerationHeader:ModelBase
    {
        [PropertyID("51120_01")]
        [ApplySecurity]
        public string payRef { get; set; }
        [PropertyID("51120_02")]
        [ApplySecurity]
        public string fiscalYear { get; set; }
        [PropertyID("51120_03")]
        [ApplySecurity]
        public string salaryType { get; set; }
        [PropertyID("51120_04")]
        [ApplySecurity]
        public string periodId { get; set; }
        [PropertyID("51120_05")]
        [ApplySecurity]
        public string payDate { get; set; }
        [PropertyID("51120_06")]
        [ApplySecurity]
        public string status { get; set; }
        [PropertyID("51120_07")]
        [ApplySecurity]
        public string notes { get; set; }
        [PropertyID("51120_08")]
        [ApplySecurity]
        public DateTime startDate { get; set; }
        [PropertyID("51120_09")]
        [ApplySecurity]
        public DateTime endDate { get; set; }
        public string payRefWithDateRange { get; set;  }
        public string payId { get; set; }

    }
}

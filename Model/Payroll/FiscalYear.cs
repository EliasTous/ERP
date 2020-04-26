using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Payroll
{
    [ClassIdentifier("51010", "51")]
    public class FiscalYear
    {
        [PropertyID("51010_01")]
        [ApplySecurity]

        public string fiscalYear { get; set; }
        [PropertyID("51010_02")]
        [ApplySecurity]
        public DateTime startDate { get; set; }
        [PropertyID("51010_03")]
        [ApplySecurity]
        public DateTime endDate { get; set; }
    }
}

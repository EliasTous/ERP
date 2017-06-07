using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51010", "51")]
    public class FiscalYear
    {
        public string fiscalYear { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
    }
}

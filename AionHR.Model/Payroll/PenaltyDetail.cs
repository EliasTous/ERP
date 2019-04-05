using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51041", "51")]
    public  class PenaltyDetail :ModelBase
    {
        public string ptId { get; set; }
        public short damage  { get; set; }
        public int sequence { get; set; }
        public short action { get; set; }
        public int? pct { get; set; }
        public bool includeTV { get; set; }

        public short? deductionType { get; set; }
        




    }
}

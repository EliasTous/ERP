using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51031", "51")]
    public class FinalEntitlementsDeductions
    {
       
        public int fsId { get; set; }
        public int? seqNo { get; set; }
        public int edId { get; set; }
        [PropertyID("51031_01")]
        [ApplySecurity]
        public int amount { get; set; }
        [PropertyID("51031_02")]
        [ApplySecurity]
        public string edName { get; set; }
        public short type { get; set; }

    }
}

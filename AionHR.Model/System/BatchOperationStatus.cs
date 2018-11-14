using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    [ClassIdentifier("20100", "20")]
    public class BatchOperationStatus
    {
        [PropertyID("20100_01")]
        [ApplySecurity]
        public int classId { get; set; }
        [PropertyID("20100_01")]
        [ApplySecurity]
        public int status { get; set; }
        [PropertyID("20100_01")]
        [ApplySecurity]
        public int tableSize { get; set; }
        [PropertyID("20100_01")]
        [ApplySecurity]
        public int processed { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
   public class FinalEntitlementsDeductions
    {
        public int fsId { get; set; }
        public int? seqNo { get; set; }
        public int edId { get; set; }
        public int amount { get; set; }
        public string edName { get; set; }
        public short type { get; set; }

    }
}

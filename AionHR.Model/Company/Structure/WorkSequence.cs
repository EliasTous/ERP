using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Structure
{
 public   class WorkSequence
    {
        public string approverPositionName  { get; set; }
        public string branchName { get; set; }
        public string departmentName { get; set; }
        public string wfId { get; set; }
        public int? seqNo { get; set; }
        public string approverPosition { get; set; }

        public string branchId { get; set; }
        public string departmentId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    public class BatchOperationStatus
    {
        public int classId { get; set; }
        public int status { get; set; }
        public int tableSize { get; set; }
        public int processed { get; set; }
    }
}

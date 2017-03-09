using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    class Attachement
    {
        public short classId { get; set; }
        public int recordId { get; set; }
        public short? seqNo { get; set; }
        public string fileName { get; set; }
    }
}

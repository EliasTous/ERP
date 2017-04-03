using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
   public class Attachement
    {
        public int classId { get; set; }
        public int recordId { get; set; }
        public short? seqNo { get; set; }
        public string fileName { get; set; }
        public string url { get; set; }

        public DateTime date { get; set; }
    }
}

using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    [ClassIdentifier("20050","20")]
   public class Attachement
    {
        public int classId { get; set; }
        public int recordId { get; set; }
        public short? seqNo { get; set; }

        [PropertyID("20050_01")]
        [ApplySecurity]
        public string fileName { get; set; }
        [PropertyID("20050_02")]
        [ApplySecurity]
        public string url { get; set; }
        [PropertyID("20050_03")]
        [ApplySecurity]

        public int? folderId { get; set; }
        [PropertyID("20050_03")]
        [ApplySecurity]
        public string folderName { get; set; }
        [PropertyID("20050_04")]
        [ApplySecurity]
        public DateTime date { get; set; }
    }
}

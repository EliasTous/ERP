using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AdminTemplates
{
    [ClassIdentifier("70304", "70")]
    public class AdminDocumentNote
    {
        public string rowId { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public int doId { get; set; }
        public string seqNo { get; set; }
        public string userId { get; set; }
        public DateTime date { get; set; }
        public string notes { get; set; }
    }
}

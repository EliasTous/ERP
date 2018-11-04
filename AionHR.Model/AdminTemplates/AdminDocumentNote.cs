using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.AdminTemplates
{
   public class AdminDocumentNote
    {
        public int rowId { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public int doId { get; set; }
        public int seqNo { get; set; }
        public int userId { get; set; }
        public DateTime date { get; set; }
        public string notes { get; set; }
    }
}

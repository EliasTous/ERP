using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Cases
{
    public  class CaseComment
    {
        public int caseId { get; set; }
        public short seqNo { get; set; }
        public string comment { get; set; }
        public int userId { get; set; }
        public DateTime date { get; set; }
    }
}

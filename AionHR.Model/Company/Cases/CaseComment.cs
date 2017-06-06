using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Company.Cases
{
    public  class CaseComment:ModelBase
    {
        
        public int userId { get; set; }
        public DateTime date { get; set; }
        public string comment { get; set; }

        public int caseId { get; set; }
        public short? seqNo { get; set; }
        public string userName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Domain;

namespace AionHR.Model.LoadTracking
{
    public class LoanComment:ModelBase
    {
        
        public int userId { get; set; }
        public DateTime date { get; set; }

        public string comment { get; set; }
        public int loanId { get; set; }
        public short? seqNo { get; set; }
        public string userName { get; set; }
    }
}

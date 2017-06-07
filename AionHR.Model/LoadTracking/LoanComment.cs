using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;

namespace AionHR.Model.LoadTracking
{
    [ClassIdentifier("45011", "45")]
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Domain;
using Model.Attributes;

namespace Model.LoadTracking
{
    [ClassIdentifier("45051", "45")]
    public class LoanComment:ModelBase
    {
        [PropertyID("45011_01")]
        [ApplySecurity]
        public int userId { get; set; }
        [PropertyID("45011_02")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("45011_03")]
        [ApplySecurity]
        public string comment { get; set; }
        public int loanId { get; set; }
        public short? seqNo { get; set; }
        [PropertyID("45011_01")]
        [ApplySecurity]
        public string userName { get; set; }
        public int employeeId { set; get; }
    }
}

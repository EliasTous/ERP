using Infrastructure.Domain;
using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Company.Cases
{
    [ClassIdentifier("43001", "43")]
    public  class CaseComment:ModelBase
    {
        [PropertyID("43001_01")]
        [ApplySecurity]
        public int userId { get; set; }
        [PropertyID("43001_02")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("43001_03")]
        [ApplySecurity]
        public string comment { get; set; }

        public int caseId { get; set; }
        public short? seqNo { get; set; }
        [PropertyID("43001_01")]
        [ApplySecurity]
        public string userName { get; set; }
    }
}

using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51017", "20")]
    public class PayrollIndemnity:ModelBase,IEntity
    {
        public string name { get; set; }
        public int minResignationDays { get; set; }
        //public string exemptMarriagePrd { get; set; }
        //public string exemptDeliveryPrd { get; set; }
    }
    [ClassIdentifier("51018", "20")]
    public class PayrollIndemnityDetails
    {
        public short from { get; set; }
     
      
        public short to { get; set; }
      
      
        public double pct { get; set; }


        public int inId { get; set; }

        public short seqNo { get; set; }
    }

}

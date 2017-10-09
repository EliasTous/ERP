using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
   public class PayrollIndemnity:ModelBase,IEntity
    {
        public string name { get; set; }
    }
    public class PayrollIndemnityDetails
    {
        public short from { get; set; }
     
      
        public short to { get; set; }
      
      
        public double pct { get; set; }


        public int inId { get; set; }

        public short seqNo { get; set; }
    }

}

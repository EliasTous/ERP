using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51040", "51")]
    public class PenaltyType :ModelBase
    {
        
        public short reason { get; set; }
        public string reasonString { get; set; }
      
        public short? timeBase { get; set; }
        public string timeBaseString { get; set; }

        public int? from { get; set; }
        public int? to { get; set; }



        public int timeCode { get; set; }
        public string timeCodeString { get; set; }
        public string name { get; set; }
    }
}

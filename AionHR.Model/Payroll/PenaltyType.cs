using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
   public class PenaltyType :ModelBase
    {
        public string edName { get; set; }
        public short reason { get; set; }
        public string reasonString { get; set; }
        public string edId { get; set; }
        public short timeBase { get; set; }
        public string timeBaseString { get; set; }
        public int duration { get; set; }
        public int timeVariationType { get; set; }
        public string timeVariationTypeString { get; set; }
        public string name { get; set; }
    }
}

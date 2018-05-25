using AionHR.Model.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51022", "51")]
  
    public class PayrollEntitlementDeduction
    {
        [PropertyID("51022_01")]
        [ApplySecurity]
        public string edId { get; set; }
        [PropertyID("51022_02")]
        [ApplySecurity]
        public double amount { get; set; }
        [PropertyID("51022_03")]
        [ApplySecurity]
        public int type { get; set; }
        
        public string payId { get; set; }
        [PropertyID("51022_01")]
        [ApplySecurity]
        public string edName { get; set; }

        public string seqNo { get; set; }
        public string edSeqNo { get; set; }

        [JsonProperty(PropertyName = "EDrecordId", Required = Required.Default)]
        [JsonIgnore]
        public string EDrecordId { get { return edId + seqNo + edSeqNo; }  }


    }
}

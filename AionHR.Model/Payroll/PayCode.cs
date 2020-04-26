using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Payroll
{
    
    [ClassIdentifier("51016", "51")]
    public class PayCode
    {
        [PropertyID("51016_01")]
        [ApplySecurity]
        public string name { get; set; }
        [PropertyID("51016_02")]
        [ApplySecurity]
        public string  payCode { get; set; }
    }
}

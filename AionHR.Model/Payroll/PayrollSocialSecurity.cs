using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51023", "51")]
    public class PayrollSocialSecurity
    {
        public string payCode { get; set; }
        public string pcName { get; set; }
        public string cpct { get; set; }
        public string amount { get; set; }
        public string cssAmount { get; set; }
        public string essAmount { get; set; }
        public string epct { get; set; }
    }
}


  



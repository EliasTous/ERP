using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    public class PayrollEntitlementDeduction
    {
        public string edName { get; set; }

        public int type { get; set; }
        public string seqNo { get; set; }
        public string payId { get; set; }
        public string edId { get; set; }

        public double amount { get; set; }

    }
}

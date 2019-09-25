using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    public class PayrollExpFunc : ModelBase
    {
        public string expressionId { get; set; }

        public string functionId { get; set; }

        public string functionName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Payroll
{
    public class PayrollBenefit : ModelBase
    {
        public string name { get; set; }
        public string bsName { get; set; }
        public string coName { get; set; }
        public string caName { get; set; }
        public string edName { get; set; }
        public int bsId { get; set; }
        public int conditionId { get; set; }
        public int calculationId { get; set; }
        public int edId { get; set; }

    }
}

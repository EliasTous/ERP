using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.LoadTracking
{
    [ClassIdentifier("45052", "45")]
    public   class LoanDeduction:ModelBase
    {
        public bool payrollDeduction { get; set; }
        public int loanId { get; set; }
        public DateTime? date { get; set; }
        public short type { get; set; }
        public double? amount { get; set; }
        public string notes { get; set; }
        public int employeeId { get; set; }


    }
}

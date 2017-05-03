using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    public class FiscalPeriod
    {
        public string fiscalYear { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public short salaryType { get; set; }

        public string periodId { get; set; }

        public short status { get; set; }


    }
}

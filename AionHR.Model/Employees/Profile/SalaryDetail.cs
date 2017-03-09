using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class SalaryDetail:ModelBase
    {
        public int salaryId { get; set; }
        public short seqNo { get; set; }
        public int edId { get; set; }
        public bool includeInTotal { get; set; }
        public string comments { get; set; }
        public double pct { get; set; }
        public double fixedAmount { get; set; }
    }
}

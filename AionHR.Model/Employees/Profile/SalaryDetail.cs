using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public enum PaymentMethod
    {
        Cash =0, Bank=1
    }

    public enum PaymentFrequency
    {
        Day = 1,
        Week = 2,
        BiWeek = 3,
        FourWeek = 4,
        Month = 5

    }

    public enum SalaryType
    {
        Day = 1,
        Week = 2,
        BiWeek = 3,
        FourWeek = 4,
        Month = 5

    }
    [ClassIdentifier("31063", "31")]
    public class SalaryDetail
    {
        
        public int edId { get; set; }

       
        public bool? includeInTotal { get; set; }

        
        
        public double pct { get; set; }
        public double fixedAmount { get; set; }
        public string comments { get; set; }
        public int pctOf { get; set; }
        public int type { get; set; }

        public int salaryId { get; set; }
        public short? seqNo { get; set; }
        public string edName { get; set; }
        public bool? isTaxable { get; set; }
    }
}

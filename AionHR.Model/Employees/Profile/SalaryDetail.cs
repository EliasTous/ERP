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
        [PropertyID("31063_01")]
        [ApplySecurity]
        public int edId { get; set; }

        [PropertyID("31063_02")]
        [ApplySecurity]
        public bool? includeInTotal { get; set; }


        [PropertyID("31063_03")]
        [ApplySecurity]
        public double pct { get; set; }
        [PropertyID("31063_04")]
        [ApplySecurity]
        public double fixedAmount { get; set; }
        [PropertyID("31063_05")]
        [ApplySecurity]
        public string comments { get; set; }
        [PropertyID("31063_06")]
        [ApplySecurity]
        public int pctOf { get; set; }
        [PropertyID("31063_07")]
        [ApplySecurity]
        public int type { get; set; }

        public int salaryId { get; set; }
        public short? seqNo { get; set; }
        [PropertyID("31063_01")]
        [ApplySecurity]
        public string edName { get; set; }
        public bool? isTaxable { get; set; }
    }
}

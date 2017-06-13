using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.System
{
    [ClassIdentifier("20081", "20")]
    public class CompanyRightToWork : ModelBase
    {
        [PropertyID("20081_01")]
        [ApplySecurity]
        public int branchId { get; set; }
        [PropertyID("20081_02")]
        [ApplySecurity]
        public int dtId { get; set; }

        [PropertyID("20081_03")]
        [ApplySecurity]
        public string documentRef { get; set; }
        [PropertyID("20081_04")]
        [ApplySecurity]
        public string remarks { get; set; }
        //[PropertyID("20081_05")]
        //[ApplySecurity]
        public bool hijriCal { get; set; }
        [PropertyID("20081_06")]
        [ApplySecurity]
        public DateTime issueDate { get; set; }
        [PropertyID("20081_07")]
        [ApplySecurity]
        public DateTime expiryDate { get; set; }
        [ApplySecurity]
        [PropertyID("20081_08")]
        public string fileUrl { get; set; }

        public string issueDateFormatted
        {
            get
            {
                if (hijriCal)
                    return issueDate.ToString("dd/MM/yyyy", new CultureInfo("ar"));
                else
                    return issueDate.ToString("dd/MM/yyyy", new CultureInfo("en"));
            }
        }

        public string expireDateFormatted
        {
            get
            {
                if (hijriCal)
                    return expiryDate.ToString("dd/MM/yyyy", new CultureInfo("ar"));
                else
                    return expiryDate.ToString("dd/MM/yyyy", new CultureInfo("en"));
            }
        }

        [PropertyID("20081_02")]
        [ApplySecurity]
        public string dtName { get; set; }
        [PropertyID("20081_01")]
        [ApplySecurity]
        public string branchName { get; set; }
    }
}

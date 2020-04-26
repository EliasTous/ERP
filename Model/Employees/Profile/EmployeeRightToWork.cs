using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31100", "31")]
    public class EmployeeRightToWork : ModelBase
    {
        [PropertyID("31100_01")]
        [ApplySecurity]
        public int dtId { get; set; }

        [PropertyID("31100_02")]
        [ApplySecurity]
        public string documentRef { get; set; }
        [PropertyID("31100_03")]
        [ApplySecurity]
        public string remarks { get; set; }
        //[PropertyID("31100_04")]
        //[ApplySecurity]
        public bool hijriCal { get; set; }
        [PropertyID("31100_05")]
        [ApplySecurity]
        public DateTime? issueDate { get; set; }
        [PropertyID("31100_06")]
        [ApplySecurity]
        public DateTime expiryDate { get; set; }
        [PropertyID("31100_07")]
        [ApplySecurity]
        public string fileUrl { get; set; }
        public int employeeId { get; set; }
        public string name { get; set; }
        public string employeeName { get; set; }

        public string employeeRef { get; set; }
        [PropertyID("31100_01")]
        [ApplySecurity]
        public string dtName { get; set; }

        public string issueDateFormatted
        {
            get
            {
                if (!issueDate.HasValue)
                    return "";
                if (hijriCal)
                    return issueDate.Value.ToString("yyyy/MM/dd", new CultureInfo("ar"));
                else
                    return issueDate.Value.ToString("yyyy/MM/dd", new CultureInfo("en"));
            }
        }

        public string expireDateFormatted
        {
            get
            {
                if (hijriCal)
                    return expiryDate.ToString("yyyy/MM/dd", new CultureInfo("ar"));
                else
                    return expiryDate.ToString("yyyy/MM/dd", new CultureInfo("en"));
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeRightToWork:ModelBase
    {
        public int employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public int dtId { get; set; }

        public string dtName { get; set; }
        public string documentRef { get; set; }
        public string remarks { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime expiryDate { get; set; }

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
        public bool hijriCal { get; set; }
        public string fileUrl { get; set; }
    }
}

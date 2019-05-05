using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    [ClassIdentifier("31051", "31")]
    public class EmployeeDocument:ModelBase
    {
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public int? dtId { get; set; }

        public string fileUrl { get; set; }
        public string dtName { get; set; }
        public string documentRef { get; set; }
        public DateTime? expiryDate { get; set; }
        public string remarks { get; set; }
    }
}

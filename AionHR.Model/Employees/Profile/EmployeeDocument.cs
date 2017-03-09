using AionHR.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeDocument:ModelBase
    {
        public int employeeId { get; set; }
        public short seqNo { get; set; }
        public int documentTypeId { get; set; }
        public DateTime issuedDate { get; set; }
        public DateTime? expiryDate { get; set; }
        public string issuedPlace { get; set; }
        public string reference { get; set; }
        public string remarks { get; set; }
        public string nameOnDocument { get; set; }
        public string documentImagePath { get; set; }
        public string jobDescription { get; set; }
        public string email { get; set; }
    }
}

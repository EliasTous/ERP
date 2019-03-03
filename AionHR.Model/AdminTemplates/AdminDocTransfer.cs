using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model
{
    [ClassIdentifier("70304", "70")]
    public class AdminDocTransfer
    {
        public string seqNo { get; set; }
       
        public int doId { get; set; }
        
        public DateTime date { get; set; }
        public string notes { get; set; }
        public string departmentName { get; set; }
        public string departmentId { get; set; }
        public string employeeId { get; set; }
        public EmployeeName employeeName { get; set; }
        public string apStatus { get; set; }
        public string statusName { get; set; }

    }
}

using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LoadTracking
{
    [ClassIdentifier("45053", "45")]
    public class LoanApproval 
    {
        public string approverName { get; set; }
        public string seqNo { get; set; }
        public string employeeName { get; set; }
        public double amount { get; set; }
        public string currencyRef { get; set; }

        public string departmentName { get; set; }
        public string loanId { get; set; }
        public int approverId { get; set; }
        public int employeeId { get; set; }

        public string notes { get; set; }
        public string statusString { get; set; }
        public short status { get; set; }
        public string apId { get; set; }
        public string apName { get; set; }
        public string arId { get; set; }
        public string arName { get; set; }
    }
}

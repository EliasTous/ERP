using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LoadTracking
{
    public class LoanApproval
    {
        public EmployeeName approverName { get; set; }
        public string departmentName { get; set; }
        public int loanId { get; set; }
        public int approverId { get; set; }
        public short status { get; set; }
        public string notes { get; set; }
        public string statusString { get; set; }
       
    }
}

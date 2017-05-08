using AionHR.Infrastructure.Domain;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LoadTracking
{
    public class Loan:ModelBase,IEntity
    {
        public int employeeId { get; set; }
        public int ltId { get; set; }
        public DateTime date { get; set; }
        public int currencyId { get; set; }
        public double amount { get; set; }
        public short payments { get; set; }
        public string purpose { get; set; }
        public short status { get; set; }
        public DateTime? effectiveDate { get; set; }

        // get
        public EmployeeName employeeName { get; set; }
        public string ltName { get; set; }
        public string currencyRef { get; set; }

        public double deductedAmount { get; set; }
    }
}

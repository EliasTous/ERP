using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.LeaveManagement
{
    [ClassIdentifier("42020", "42")]
    public class LeaveRequest:ModelBase
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string employeeId { get; set; }

        public string justification { get; set; }

        public string destination { get; set; }
        public bool? isPaid { get; set; }

        public int? ltId { get; set; }



        public short status { get; set; }

       

        public DateTime? returnDate { get; set; }

      
        

        public string leavePeriod { get; set; }

        public string employeeRef { get; set; }
        public EmployeeName employeeName { get; set; }

        public string ltName { get; set; }
    }   
}

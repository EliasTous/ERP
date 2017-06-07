using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees
{
    [ClassIdentifier("44000", "44")]
    public class Complaint : ModelBase,IEntity
    {
        public int employeeId { get; set; }
        public DateTime? dateReceived { get; set; }
        public string actionTaken { get; set; }
        public string actionRequired { get; set; }
        public string complaintDetails { get; set; }
        public short status { get; set; }

        public EmployeeName employeeName { get; set; }
    }
}

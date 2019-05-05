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
        [PropertyID("44000_01")]
        [ApplySecurity]
        public int employeeId { get; set; }
        [PropertyID("44000_02")]
        [ApplySecurity]
        public DateTime? dateReceived { get; set; }
        [PropertyID("44000_03")]
        [ApplySecurity]
        public string actionTaken { get; set; }
        [PropertyID("44000_04")]
        [ApplySecurity]
        public string actionRequired { get; set; }
        [PropertyID("44000_05")]
        [ApplySecurity]
        public string complaintDetails { get; set; }
        [PropertyID("44000_06")]
        [ApplySecurity]
        public short status { get; set; }

        [PropertyID("44000_01")]
        [ApplySecurity]
        public string employeeName { get; set; }
    }
}

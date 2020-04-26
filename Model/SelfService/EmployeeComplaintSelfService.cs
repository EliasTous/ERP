using Infrastructure.Domain;
using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.SelfService
{
    [ClassIdentifier("60107", "60")]
    public  class EmployeeComplaintSelfService : ModelBase , IEntity
    {
       
    
        public int? employeeId { get; set; }

      
        public DateTime? dateReceived { get; set; }
     
        public string actionTaken { get; set; }
        [PropertyID("60107_02")]
        [ApplySecurity]
        public string actionRequired { get; set; }
        [PropertyID("60107_03")]
        [ApplySecurity]
        public string complaintDetails { get; set; }
     
        public short? status { get; set; }

     
        public string employeeName { get; set; }
    }
}

using AionHR.Infrastructure.Domain;
using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using AionHR.Model.LeaveManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.SelfService
{
    [ClassIdentifier("60104", "60")]
    public class leaveRequetsSelfservice : ModelBase
    {

        [PropertyID("60104_01")]
        [ApplySecurity]
        public DateTime startDate { get; set; }
        [PropertyID("60104_02")]
        [ApplySecurity]
        public DateTime endDate { get; set; }
      
        public string employeeId { get; set; }

        [PropertyID("60104_03")]
        [ApplySecurity]
        public string justification { get; set; }

        [PropertyID("60104_04")]
        [ApplySecurity]
        public string destination { get; set; }
       
        public bool? isPaid { get; set; }
        [PropertyID("60104_05")]
        [ApplySecurity]
        public int? ltId { get; set; }

        

        public short apStatus { get; set; }

       

        public DateTime? returnDate { get; set; }
       
        public string returnNotes { get; set; }

       
        public string leaveRef { get; set; }

        public string leavePeriod { get; set; }


        public string employeeRef { get; set; }
       
        public string employeeName { get; set; }
       
        public string ltName { get; set; }

        public string departmentName { get; set; }

        public string branchName { get; set; }

        public double? leaveDays { get; set; }
        //public int? workingHours { get; set; }
    //    public int? leaveHours { get; set; }

        public string replacementName { get; set; }
        public string replacementId { get; set; }
    }
}

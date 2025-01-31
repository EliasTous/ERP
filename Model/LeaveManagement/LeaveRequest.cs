﻿using Infrastructure.Domain;
using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.LeaveManagement
{
    [ClassIdentifier("42020", "42")]
    public class LeaveRequest:ModelBase
    {
        [PropertyID("42020_01")]
        [ApplySecurity]
        public DateTime startDate { get; set; }
        [PropertyID("42020_02")]
        [ApplySecurity]
        public DateTime endDate { get; set; }
        [PropertyID("42020_03")]
        [ApplySecurity]
        public string employeeId { get; set; }
        [PropertyID("42020_04")]
        [ApplySecurity]

        public string justification { get; set; }
        [PropertyID("42020_05")]
        [ApplySecurity]

        public string destination { get; set; }
        [PropertyID("42020_06")]
        [ApplySecurity]
        public bool? isPaid { get; set; }
        [PropertyID("42020_07")]
        [ApplySecurity]

        public int? ltId { get; set; }

        [PropertyID("42020_08")]
        [ApplySecurity]

        public short apStatus { get; set; }
        public string statusString { get; set; }

        [PropertyID("42020_09")]
        [ApplySecurity]

        public DateTime? returnDate { get; set; }
        [PropertyID("42020_10")]
        [ApplySecurity]

        public string returnNotes { get; set; }

        [PropertyID("42020_11")]
        [ApplySecurity]

        public string leaveRef { get; set; }

        public string leavePeriod { get; set; }


        public string employeeRef { get; set; }
        [PropertyID("42020_03")]
        [ApplySecurity]
        public string employeeName { get; set; }
        [PropertyID("42020_07")]
        [ApplySecurity]
        public string ltName { get; set; }

        public string departmentName { get; set; }

        public string branchName { get; set; }
        public string ltRef { get; set; }
        public double? leaveDays { get; set; }
    //    public int? workingHours { get; set; }
      //  public int? leaveHours { get; set; }
        public string replacementId { get; set; }

        public string replacementName { get; set; }
        public string replApStatus { get; set; }


    }   
}

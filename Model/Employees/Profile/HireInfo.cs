﻿using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31141","31")]
   public class HireInfo

    {

        public string employeeId { get; set; }
        public string employeeName { get; set; }
        [PropertyID("31141_01")]
        [ApplySecurity]
        public string npName { get; set; }
        [PropertyID("31141_02")]
        [ApplySecurity]
        public DateTime? probationEndDate { get; set; }
        [PropertyID("31141_03")]
        [ApplySecurity]
        public DateTime? nextReviewDate { get; set; }
        [PropertyID("31141_01")]
        [ApplySecurity]
        public int? npId { get; set; }
        [PropertyID("31141_04")]
        [ApplySecurity]
        public DateTime? termEndDate { get; set; }
        [PropertyID("31141_05")]
        [ApplySecurity]
        public string recruitmentInfo { get; set; }
        [PropertyID("31141_06")]
        [ApplySecurity]
        public string recruitmentCost { get; set; }
        public string pyReference { set; get;}
        public string taReference { set; get; }
        public DateTime pyActiveDate { set; get; }

        public int? recBranchId { set; get; }
        [PropertyID("31141_02")]
        [ApplySecurity]
        public short? probationPeriod { set; get; }

        public DateTime? hireDate { get; set; }

        public int? sponsorId { set; get; }
        public int? prevRecordId { set; get; }
        public string otherRef { set; get; }

        public int? bsId { set; get; }


        public int? languageId { get; set; }
        public int? caId { get; set; }
        

    }
}

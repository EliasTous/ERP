﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.AdminTemplates
{
    [ClassIdentifier("70301", "70")]
    public class AdminDocument : ModelBase
    {
        public string status { get; set; }
        public string statusName { get; set; }
        public string bpName { get; set; }
        public string dcName { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime expiryDate { get; set; }

        public int bpId { get; set; }
        public string docRef { get; set; }
        public string binNo { get; set; }
        public int dcId { get; set; }
        public string notes { get; set; }
        public string oDocRef { get; set; }
        public int? languageId { get; set; }

        public string employeeName { get; set;}

        public string departmentId { get; set; }
        public string employeeId { get; set; }

        public string departmentName { get; set; }


    }
}

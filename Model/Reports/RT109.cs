﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80109", "80")]
    public class RT109
    {
        public string expiryDateStringFormat { get; set; }

        public string employeeName{get; set;}
       
        public DateTime expiryDate
        {
            set;
            get;
        }

        public string dtName { get; set; }
        public string documentRef { get; set; }
        public string branchName { get; set; }
        public string departmentName { get; set; }

        public string positionName { get; set; }


        public int days { get; set; }
    }
}

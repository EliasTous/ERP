﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80102", "80")]
    public class RT102B
    {
        public string employeeName
        {
            get; set;
        }
        public string departmentName { get; set; }
        public string branchName { get; set; }
        public string positionName { get; set; }
        public string divisionName { get; set; }

        public DateTime date { get; set; }

        public string esName { get; set; }
        public String DateString { get; set; }
        public string trName { get; set; }
      
    }
}

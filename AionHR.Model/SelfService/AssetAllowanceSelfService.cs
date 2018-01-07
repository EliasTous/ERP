﻿using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.SelfService
{
   public class AssetAllowanceSelfService :ModelBase
    {
       
        public EmployeeName employeeName { get; set; }
       
        public int acId { get; set; }

       
        public string description { get; set; }
       
        public string serialNo { get; set; }
     
        public string comment { get; set; }
       
        public DateTime date { get; set; }
      
        public DateTime returnedDate { get; set; }
      
        public int employeeId { get; set; }
      
        public string acName { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Employees.Profile
{
    public class EmployeeTermination:ModelBase
    {

        public DateTime date { get; set; }
        public int ttId { get; set; }
        public int trId { get; set; }
       
        
        public int rehire { get; set; }
        

        public int employeeId { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
    public class Router:ModelBase
    {
        public string routerRef { get; set; }
        public bool isInactive { get; set; }
        public int branchId { get; set; }
        

        public string branchName { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
 public   class RT309
    {
        public string employeeId { get; set; }
        public string employeeName { get; set; }
        public string dayId { get; set; }
        public List<ShiftLog> shiftLog { get; set; }
        public string shiftId { get; set; }
        public DateTime dayIdDateTime { get; set; }


    }
}

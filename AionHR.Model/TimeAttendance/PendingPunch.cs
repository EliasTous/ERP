﻿using AionHR.Model.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
    public class PendingPunch : Check
    {
        public string recordId { get; set; }

        public string ppTypeName { get; set; }

        public string employeeName { get; set; }

    }
}

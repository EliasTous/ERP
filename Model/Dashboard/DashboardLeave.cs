﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
    public class DashboardLeave
    {
       
        public int leaveId { get; set; }
        public int approverId { get; set; }
        public short status { get; set; }
        public string notes { get; set; }
        public string arId { get; set; }
        public string arName { get; set; }
        public string seqNo { get; set; }
        public string activityId { get; set; }
    }
}

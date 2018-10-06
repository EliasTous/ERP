﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Services.Messaging.TimeAttendance
{
 public  class DashboardTimeListRequest:ListRequest
    {
        public int approverId { get; set; }
        public int employeeId { get; set; }
        public string dayId { get; set; }
        public string timeCode { get; set; }
        public string shiftId { get; set; }

     
        public override Dictionary<string, string> Parameters
        {
            get
            {
                parameters = base.Parameters;
                parameters.Add("_approverId", approverId.ToString());
                parameters.Add("_employeeId", employeeId.ToString());
                parameters.Add("_dayId", dayId);
                parameters.Add("_timeCode", timeCode);
                parameters.Add("_shiftId", shiftId);





                return parameters;
            }
        }
    }
}

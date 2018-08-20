﻿using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.TimeAttendance
{
  public  class Time
    {
        public EmployeeName employeeName { set; get; }
        public string dayId { set; get;  }
        public DateTime? dayIdDate
        {
            get
            {
                if (String.IsNullOrEmpty(dayId))
                    return null; 
                return DateTime.ParseExact(dayId, "yyyyMMdd", new CultureInfo("en"));
            }
            set { value = dayIdDate; }
        }
        public int employeeId { get; set; }
        public string timeCode { set; get; }
        public string timeCodeString { set; get; }


        public int  approverId { get; set; }

        public short status { get; set; }
        public string notes { set; get; }

    }
}

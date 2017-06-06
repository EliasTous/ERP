﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Attendance
{
   public class AttendanceScheduleDay
    {

        public string dayTypeId { get; set; }
        public string firstIn { get; set; }
        public string lastOut { get; set; }
        public int scId { get; set; }
        public short dow { get; set; }
   

        

        public string duration { get; set; }

        public string durationFormatted
        {
            get
            {
                int hours;
                int mins;
                int durationMins = Convert.ToInt32(duration);
                hours = durationMins / 60;
                mins = durationMins % 60;
                return string.Format(hours.ToString().PadLeft(2, '0') + ":" + mins.ToString().PadLeft(2, '0'));
            }
        }
    }
}

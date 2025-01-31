﻿using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80303", "80")]
    public class RT303A
    {
        public string dayId { get; set; }

        public int dow { get; set; }

        public string dowString { get; set; }

        public string dateString { get; set; }

        public bool isWorkingDay { get; set; }



        public string workingHours
        {
            get; set;
        }
        public int calHours
        {
            get; set;
        }
        public string paidLeaves { get; set; }
        public string unpaidLeaves { get; set; }

        public string LeaveType { get; set; }
        public string jobTasks { get; set; }

        public int dayStatus { get; set; }
        public string dayStatusString { get; set; }
        public string specialTasks { get; set; }

        public string checkIn1 { get; set; }
        public string checkIn2 { get; set; }
        public string checkIn3 { get; set; }
        public string checkIn4 { get; set; }


        public string checkOut1 { get; set; }

        public string checkOut2 { get; set; }

        public string checkOut3 { get; set; }

        public string checkOut4 { get; set; }

        public TimeSpan OL_A { get; set; }
        public TimeSpan OL_D { get; set; }

        public TimeSpan OL_B { get; set; }

        public TimeSpan OL_NET { get; set; }

        public string breaks { get; set; }

        public string overtime
        {
            get; set;
        }

        public string lateness { get; set; }

        public string lateness_A { get; set; }

        public string lateness_B { get; set; }

        public string lateness_D { get; set; }
        public string scName { get; set; }




    }
}
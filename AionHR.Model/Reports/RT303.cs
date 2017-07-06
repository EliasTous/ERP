using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    public class RT303
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




    }
}

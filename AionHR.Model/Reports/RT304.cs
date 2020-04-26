using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80304", "80")]
    public class RT304
    {
        public int dow { get; set; }

        public TimeSpan from { get; set; }

        public TimeSpan to { get; set; }

        public bool active { get; set; }

        public int headCount { get; set; }
    }

    public class Period
    {
        public int periodId { get; set; }

        public string title { get; set; }

        public int count { get; set; }
    }

    public class WorkDay
    {
        public int dow { get; set; }

        public List<Period> periods { get; set; }
    }
    public class departmentAvailability
    {
        public string period {
            get { return from.ToString(@"hh\:mm"); }
        }

        public TimeSpan from { get; set; }
        public TimeSpan to { get; set; }


        public int day1 { get; set; }
        public int day2 { get; set; }
        public int day3 { get; set; }
        public int day4 { get; set; }
        public int day5 { get; set; }
        public int day6 { get; set; }
        public int day7 { get; set; }
    }
}

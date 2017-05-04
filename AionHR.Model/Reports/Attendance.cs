using AionHR.Model.Employees.Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AionHR.Model.Reports
{
    public class EmployeeAttendances

    {
        public string name { get; set; }
        public string branchName { get; set; }

        public string departmentName { get; set; }

        public string positionName { get; set; }
        private List<Attendance> listAts;
        public AttendanceCollection Attendances
        {
            get
            {
                return new AttendanceCollection(listAts);
            }
        }

        public int DaysIn
        {
            get
            {
                return daysIn;
            }
        }
        private int daysIn;
        

        public void Add(Attendance at)
        {

            int index = listAts.IndexOf(at);
            if (index == -1)
            {
                listAts.Add(at);
                return;
            }
            listAts[index].timeIn = at.timeIn;
            listAts[index].timeOut = at.timeOut;
            listAts[index].workingTime = at.workingTime;


            daysIn++;
        }

        private void FillAttendancesEmpty(string y,string m)
        {
            int month = Convert.ToInt32(m);
            int year = Convert.ToInt32(y);
            for (int i=0;i<DateTime.DaysInMonth(year,month);i++)
            {
                Attendances.Add(new Attendance() { timeIn = "", timeOut = "", day = (i + 1).ToString() });
            }
        }

        public EmployeeAttendances()
        {
            listAts = new List<Attendance>();
            
        }

        public EmployeeAttendances(HashSet<int> days):this()
        {
            foreach (var item in days)
            {
                Add(new Attendance() { timeIn = "", timeOut = "", day = item.ToString()});
            }
        }
    }

   

    public class Attendance
    {
        public string timeIn { get; set; }

        public string timeOut { get; set; }

        public string day { get; set; }

        public string month { get; set; }

        public string year { get; set; }

        public string workingTime { get; set; }

        public override Boolean Equals(Object obj)
        {
            return Convert.ToInt32(this.day) == Convert.ToInt32((obj as Attendance).day);
        }

    }

    public class DailyAttendance
    {
        public string name { get; set; }
        public string branchName { get; set; }

        public string departmentName { get; set; }

        public string positionName { get; set; }

        public string divisionName { get; set; }

        public string DateString { get; set; }

        public DateTime Date { get; set; }

        public string DOW { get; set; }
        

        public TimeSpan workingHours { get; set; }

        public TimeSpan lateness { get; set; }

        public TimeSpan early { get; set; }
    }
}
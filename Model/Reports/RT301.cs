using Model.Attributes;
using Model.Employees.Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Reports
{
    [ClassIdentifier("80301", "80")]
    public class RT301
    {
        public string name { get; set; }
        public string branchName { get; set; }
        
        public string departmentName { get; set; }

        public string positionName { get; set; }

        public string checkIn { get; set; }

        public string checkOut { get; set; }

        public string dayId
        {
            get {

                return year+month+day;
            }
            set
            {
                year = value.Substring(0, 4);
                month = value.Substring(4, 2);
                day = value.Substring(6, 2);
                
            }
        }

        public string workingTime { get; set; }

        public string day { get; set; }

        public string month { get; set; }

        public string year { get; set; }

        public string OL_A { get; set; }

        public string OL_D { get; set; }

        public string OL_A_SIGN { get; set; }

        public string OL_D_SIGN { get; set; }

    }
    
    public class MonthAttendance
    {
       
        

        public string Month { get; set; }

        
        public DaysCollection Days { get; set; }

       public EmployeeAttendanceCollection EmployeeAttendances { get; set; }

        public string Name { get; set; }

        public string departmentName { get; set; }

        public string branchName { get; set; }

        public string positionName { get; set; }

        public MonthAttendance(String key, List<RT301> list)
        {
            this.Month = key;
            HashSet<int> days = new HashSet<int>();
            Days = new DaysCollection();
            list.ForEach(x => { days.Add(Convert.ToInt32(x.day));  });

            days.ToList().ForEach(x => Days.Add(new Day() { DayNumber=x })); 
      
            var grouped = list.GroupBy(x => x.name);


            EmployeeAttendances = new EmployeeAttendanceCollection();
            foreach (var item in grouped)
            {
                EmployeeAttendances at = new EmployeeAttendances(days);
                at.name = item.Key;

                var details = item.ToList();
                if (details.Count != 0)
                {
                    at.departmentName = details[0].departmentName;
                    at.branchName = details[0].branchName;
                    at.positionName = details[0].positionName;
                }

                foreach (var subItem in item.ToList())
                {
                    at.Add(new Attendance() { workingTime = subItem.workingTime, day = subItem.day, year = subItem.year, month = subItem.month, timeIn = subItem.checkIn, timeOut = subItem.checkOut });

                }
                EmployeeAttendances.Add(at);
            }
        }
    }

}

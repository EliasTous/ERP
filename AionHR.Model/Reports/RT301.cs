using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
   public class RT301
    {
        public EmployeeName name { get; set; }
        public string branchName { get; set; }
        
        public string departmentName { get; set; }

        public string positionName { get; set; }

        public string checkIn { get; set; }

        public string checkOut { get; set; }

        public string dayId
        {
            get {

                return "";
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

    }

    
}

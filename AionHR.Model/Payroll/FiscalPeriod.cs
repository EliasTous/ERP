using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    public class FiscalPeriod
    {
        
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public short salaryType { get; set; }

        

        public short status { get; set; }
        public string periodId { get; set; }

        public string fiscalYear { get; set; }

        public string GetFriendlyName(string month, string week,string weeks,string format)
        {
            int id = Convert.ToInt32(periodId);
            switch(salaryType)
            {
                case (int)SalaryType.Week: return week  +" " + id + " ( " + startDate.ToString(format) + " - " + endDate.ToString(format) + " )";
                case (int)SalaryType.BiWeek: return weeks + " " + ((id*2)-1).ToString() + ", "+ ((id * 2)).ToString() +" ( "+ startDate.ToString(format) + " - " + endDate.ToString(format)+" )";
                case (int)SalaryType.FourWeek: return weeks + " " + ((id * 4) - 3).ToString() + ", " + ((id * 4) -2).ToString() + ", " + ((id * 4) - 1).ToString() + ", " + ((id * 4)).ToString()+ " ( "+startDate.ToString(format) + " - " + endDate.ToString(format) + " )"; ;
                case (int)SalaryType.Month: return month + " " + id.ToString() + " ( " + startDate.ToString(format) + " - " + endDate.ToString(format) + " )";
                default: return periodId;
            }
        }

    }
}

using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51011", "51")]
    public class FiscalPeriod
    {
        [PropertyID("51011_01")]
        [ApplySecurity]

        public DateTime startDate { get; set; }
        [PropertyID("51011_02")]
        [ApplySecurity]

        public DateTime endDate { get; set; }
        [PropertyID("51011_03")]
        [ApplySecurity]

        public short salaryType { get; set; }


        [PropertyID("51011_04")]
        [ApplySecurity]

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

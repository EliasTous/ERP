using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.SelfService
{
    [ClassIdentifier("60111", "60")]
    public class EmployeePayrollSelfService
    {
       
        public EmployeeName name { get; set; }
        //[PropertyID("51021_02")]
        //[ApplySecurity]
        //public string branchName { get; set; }
       
        public string departmentName { get; set; }
       

        public string currencyRef { get; set; }
       
        public string calendarDays { get; set; }
      
        public string calendarMinutes { get; set; }
        
        public string workingDays { get; set; }
       
        public string workingMinutes { get; set; }
       
        public double? basicAmount { get; set; }
      
        public double? taxAmount { get; set; }
     
        public double? netSalary { get; set; }
      

        public double? dAmount { get; set; }
       
        public double eAmount { get; set; }
        

        public string currencyName { get; set; }
      
        public double? cssAmount { get; set; }
      
        public double? essAmount { get; set; }

        public string seqNo { get; set; }


        public string payId { get; set; }
        public string employeeId { get; set; }
    }
 
}

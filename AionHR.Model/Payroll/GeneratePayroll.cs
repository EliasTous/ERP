using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
    [ClassIdentifier("51121", "51")]
    public class GeneratePayroll
    {
     public  int payId { set; get;  }
       public int employeeId { set; get; }
        public int departmentId { set; get; }
        public int branchId { set; get; }
       

    }
}

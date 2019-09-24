using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Payroll
{
   public class CheckExpression
    {
        public string expression { get; set; }
        public bool success { get; set; }
        public string returnMessage { get; set; }
    }
}

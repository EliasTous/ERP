using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
   public  class CompanyRTW
    {

        public string dtName { get; set; }

        public string documentRef { get; set; }

        public DateTime expiryDate { get; set; }

        public int days { get; set; }
    }
}

using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Dashboard
{
    [ClassIdentifier("81106", "81")]
    public  class CompanyRTW
    {
     
        public string dtName { get; set; }

        public string documentRef { get; set; }

        public DateTime expiryDate { get; set; }
        public string expiryDateString { get; set; }

        public int days { get; set; }
    }
}

using AionHR.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Reports
{
    [ClassIdentifier("80112", "80")]
    public  class RT113 :ModelBase
    {

        public DateTime? licenseIssueDate { get; set; }
        public string licenseIssueDateString { get; set; }
        public DateTime? licenseExpiryDate { get; set; }
        public string licenseExpiryDateString { get; set; }
        public int localsMale { get; set; }
        public int localsFemale { get; set; }
        public int foreignersMale { get; set; }
        public int foreignersFemale { get; set; }
        public int probationLocalsMale { get; set; }
        public int probationLocalsFemale { get; set; }
        public int probationForeignersMale { get; set; }
        public int probationForeignersFemale { get; set; }

        public int supplierLocalsMale { get; set; }
        public int supplierLocalsFemale { get; set; }
        public int supplierForeignersMale { get; set; }
        public int supplierForeignersFemale { get; set; }
        public int males { get; set; }
        public int females { get; set; }
        public int total { get; set; }
        public string name { get; set; }
    }
}

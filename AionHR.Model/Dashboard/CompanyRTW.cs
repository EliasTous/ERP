using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dashboard
{
    [ClassIdentifier("81106", "81")]
    public  class CompanyRTW
    {
        public int days { get; set; }
        public string dtName { get; set; }

        public string branchName { get; set; }
        public string fileUrl { get; set; }
        public string fileName { get; set; }
        public int dtId { get; set; }
        public string branchId { get; set; }
        public string documentRef { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime expiryDate { get; set; }
        public string expiryDateString { get; set; }
        public bool hijriCal { get; set; }
        public string remarks { get; set; }




    }
}

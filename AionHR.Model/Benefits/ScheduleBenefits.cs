using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Benefits
{
 public  class ScheduleBenefits
    {
        public string benefitName { get; set; }
        public int bsId { get; set; }
        public int benefitId { get; set; }
        public short? aqType { get; set; }
        public UInt32? intervalDays { get; set; }
        public UInt32? defaultAmount { get; set; }
        public bool isChecked { get; set; }

    }
}

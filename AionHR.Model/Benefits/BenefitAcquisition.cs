using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.Benefits
{
    [ClassIdentifier("25101", "25")]
    public class BenefitAcquisition :ModelBase
    {
        public EmployeeName employeeName { get; set; }

        public string benefitName { get; set; }
        public string bsName { get; set; }
        public string period { get; set; }

        public int employeeId { get; set; }

        public int bsId { get; set; }
        public int benefitId { get; set; }
        public short? aqType { get; set; }
        public DateTime aqDate { get; set; }

        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }

        public bool? isHijriDate { get; set; }
        public int amount { get; set; }
        public double? aqRatio { get; set; }

        public int? aqAmount { get; set; }

        public int? amountDue { get; set; }

        public string notes { get; set; }
        public short deliveryType { get; set; }
      
    }
}

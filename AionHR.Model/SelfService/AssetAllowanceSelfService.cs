using AionHR.Model.Attributes;
using AionHR.Model.Employees.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Model.SelfService
{
    [ClassIdentifier("60108", "60")]
    public class AssetAllowanceSelfService :ModelBase
    {
       
        public string employeeName { get; set; }
        [PropertyID("60108_02")]
        [ApplySecurity]
        public int acId { get; set; }

        [PropertyID("60108_03")]
        [ApplySecurity]
        public string description { get; set; }
        [PropertyID("60108_04")]
        [ApplySecurity]
        public string serialNo { get; set; }
        [PropertyID("60108_05")]
        [ApplySecurity]
        public string comment { get; set; }
        [PropertyID("60108_06")]
        [ApplySecurity]
        public DateTime date { get; set; }
        [PropertyID("60108_07")]
        [ApplySecurity]
        public DateTime returnedDate { get; set; }
      
        public int employeeId { get; set; }
      
        public string acName { get; set; }
    }
}

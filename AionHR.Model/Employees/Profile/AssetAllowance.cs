using Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Employees.Profile
{
    [ClassIdentifier("31011", "31")]
    public class AssetAllowance:ModelBase
    {
        [PropertyID("31011_01")]
        [ApplySecurity]
        public string employeeName { get; set; }
        [PropertyID("31011_02")]
        [ApplySecurity]
        public int acId { get; set; }

        [PropertyID("31011_03")]
        [ApplySecurity]
        public string description { get; set; }
        [PropertyID("31011_04")]
        [ApplySecurity]
        public string serialNo { get; set; }
        [PropertyID("31011_05")]
        [ApplySecurity]
        public string comment { get; set; }
        [PropertyID("31011_06")]
        [ApplySecurity]
        public DateTime? date { get; set; }
        [PropertyID("31011_07")]
        [ApplySecurity]
        public DateTime returnedDate { get; set; }
        [PropertyID("31011_01")]
        [ApplySecurity]
        public int employeeId { get; set; }
        [PropertyID("31011_02")]
        [ApplySecurity]
        public string acName { get; set; }

    }
}
